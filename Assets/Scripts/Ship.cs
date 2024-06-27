using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship
{
    public string Name;
    public string Type;
    public string Fleet;
    public ShipTraits Traits;

    public Ship(string name, string type)
    {
        Name = name;
        Type = type;
    }


    public void Initialize()
    {
        Traits = ShipDictionary.AllShips[Type];
    }

}
