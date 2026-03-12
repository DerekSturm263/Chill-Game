using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlaceableObjectInstanceData
{
    public PlaceableObject obj;
    public Vector3 position;
    public Quaternion rotation;
}

[Serializable]
public struct RoomData
{
    public Vector3Int roomDimensions;
    public Color wallpaperColor;
    public Wallpaper wallpaper;
    public List<PlaceableObjectInstanceData> objects;
}

[Serializable]
public struct PetData
{
    public Animal pet;
    public string name;
    public DateTime birthdate;
}

[Serializable]
public struct FamilyData
{
    public string yourName;
    public string partnersName;
    public List<PetData> pets;
}

[Serializable]
public struct SaveData
{
    public float level;
    public int basicCurrency;
    public int premiumCurrency;
    public SerializedDictionary<Asset, int> inventory;
    public FamilyData familyData;
    public RoomData roomData;
}
