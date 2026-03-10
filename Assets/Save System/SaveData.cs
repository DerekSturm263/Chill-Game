using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SaveData
{
    public string username;
    public float level;
    public int basic_currency_temp; // TODO: Move to Unity Cloud.
    public int premium_currency_temp; // TODO: Move to Unity Cloud.
    public SerializedDictionary<Asset, int> inventory;
    public Vector3Int roomDimensions;
    public List<ObjectInstanceData> objects;
}
