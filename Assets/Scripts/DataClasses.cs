using System.Collections.Generic;
using System;

public static class DataClasses
{
    [Serializable]
    public class PlayerData
    {
        public Floatilla Floatilla;
        public Dictionary<Location, Ledger> Wallet;
    }

    [Serializable]
    public class Location
    {
        public string HexID;
        public string Name;
        public string Type;
    }

    [Serializable]
    public class Ledger
    {

    }

    [Serializable]
    public class Fleet
    {
        public Location Location;
    }
}
