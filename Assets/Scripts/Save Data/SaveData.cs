using AYellowpaper.SerializedCollections;
using System;

[Serializable]
public struct SaveData
{
    public string name;
    public SerializedDictionary<Asset, int> inventory;
}
