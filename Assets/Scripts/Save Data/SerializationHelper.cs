using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public static class SerializationHelper
{
    private const string SaveDataPath = "";

    public static void Save<T>(T value, string fileName, byte[] key)
    {
        string path = CreateDirectory(fileName);
        string json = JsonUtility.ToJson(value, true);

        FileMode writeMode = File.Exists(path) ? FileMode.Truncate : FileMode.Create;
        using FileStream stream = new(path, writeMode);

        using Aes aes = Aes.Create();
        aes.Key = key;

        byte[] iv = aes.IV;
        stream.Write(iv, 0, iv.Length);

        using CryptoStream crypto = new(stream, aes.CreateEncryptor(), CryptoStreamMode.Write);
        using StreamWriter writer = new(crypto);

        writer.Write(json);
        writer.Flush();

        Debug.Log($"{typeof(T).Name} has been successfully saved to {path} as: {json}");
    }

    public static T Load<T>(T defaultValue, string fileName, byte[] key)
    {
        string path = CreateDirectory(fileName);

        if (!File.Exists(path))
        {
            Debug.Log($"{path} could not be found. Loading the default {typeof(T).Name}: {JsonUtility.ToJson(defaultValue, true)}");
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

        using CryptoStream crypto = new(stream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read);
        using StreamReader reader = new(crypto);

        string json = reader.ReadToEnd();
        Debug.Log($"{typeof(T).Name} has been successfully loaded from {path} as: {json}");

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
