// need to eventually account for multiple colonizables and anomalies in one hex
// baked in holders for 3 of each. Might refactor later
using System;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

[Serializable]
public class PrefabDictionary : SerializableDictionaryBase<HexType, HexList> { }

public class MapManager : MonoBehaviour
{
    
}



[Serializable]
public class HexList
{
    public List<Hex> list = new List<Hex>();
}
