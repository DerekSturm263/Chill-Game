using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public static class SerializationHelper
{
    private const string SaveDataPath = "SaveData";

    private static readonly byte[] Key =
    {
        0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
        0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
    };

    public static void Save<T>(T value, string fileName)
    {
        string path = CreateDirectory(fileName);
        string json = JsonUtility.ToJson(value, true);

        FileMode writeMode = File.Exists(path) ? FileMode.Truncate : FileMode.Create;
        using FileStream stream = new(path, writeMode);

        using Aes aes = Aes.Create();
        aes.Key = Key;

        byte[] iv = aes.IV;
        stream.Write(iv, 0, iv.Length);

        using CryptoStream crypto = new(stream, aes.CreateEncryptor(), CryptoStreamMode.Write);
        using StreamWriter writer = new(crypto);

        writer.Write(json);
        writer.Flush();

        Debug.Log($"{nameof(T)} has been successfully saved as: {json}");
    }

    public static T Load<T>(T defaultValue, string fileName)
    {
        string path = CreateDirectory(fileName);

        if (!File.Exists(path))
        {
            Debug.Log($"{nameof(T)} could not be found. Loading the default: {JsonUtility.ToJson(defaultValue)}");
            return JsonUtility.FromJson<T>(JsonUtility.ToJson(defaultValue));
        }

        using FileStream stream = new(path, FileMode.Open);
        using Aes aes = Aes.Create();

        byte[] iv = new byte[aes.IV.Length];
        int numBytesToRead = aes.IV.Length;
        int numBytesRead = 0;

        while (numBytesToRead > 0)
        {
            int n = stream.Read(iv, numBytesRead, numBytesToRead);
            if (n == 0)
                break;

            numBytesRead += n;
            numBytesToRead -= n;
        }

        using CryptoStream crypto = new(stream, aes.CreateDecryptor(Key, iv), CryptoStreamMode.Read);
        using StreamReader reader = new(crypto);

        string json = reader.ReadToEnd();
        Debug.Log($"{nameof(T)} has been successfully loaded as: {json}");

        return JsonUtility.FromJson<T>(json);
    }

    public static void Delete(string fileName)
    {
        string path = CreateDirectory(fileName);

        if (File.Exists(path))
            File.Delete(path);

        Debug.Log($"All Save Data erased from {path}");
    }

    public static string CreateDirectory(string fileName)
    {
        string directory = Path.Combine(Application.persistentDataPath, SaveDataPath);

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        return Path.Combine(directory, fileName);
    }
}
