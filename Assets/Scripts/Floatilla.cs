using System.Collections.Generic;
using System;
using UnityEngine;
using static DataClasses;

[Serializable]
public class Floatilla
{
    public Floatilla(ErrorWindow ew)
    {
        ErrorWindow = ew;
    }

    public ErrorWindow ErrorWindow;
    public List<Ship> Ships = new List<Ship>();
    public Dictionary<Ship, ConstructionStatus> ShipYard;

    //this for adding default ships with default traits
    public bool AddShip(string name, string type)
    {
        if (!NameIsUnique(name)) name = CreateUniqueName(name);

        var ship = new Ship(name, type);
        ship.Initialize();

        Ships.Add(ship);
        return false;
    }

    public void AddShip(Ship ship)
    {
        if (!NameIsUnique(ship.Name)) ship.Name = CreateUniqueName(ship.Name);

        Ships.Add(ship);
    }

    public void AddShip(Ship ship, string name)
    {
        ship.Name = name;
        AddShip(ship);
    }

    //private bool NameIsUnique(string name)
    //{
    //    foreach (Ship s in Ships)
    //    {
    //        if (s.Name == name)
    //        {
    //            ErrorWindow.ValidationError("Please choose a unique ship name");
    //            return false;
    //        }
    //    }
    //    return true;
    //}
    private bool NameIsUnique(string name)
    {
        foreach (Ship s in Ships)
        {
            if (s.Name == name) return false;
        }
        return true;
    }

    private string CreateUniqueName(string name)
    {
        foreach (Ship s in Ships)
        {
            if (s.Name == name)
            {
                name = CreateUniqueName(name + " Copy");
            }
        }
     return name;
    }
}


[Serializable]
public class ConstructionStatus
{
    public (int, int) TimeStarted;
    public int SegmentsRemaining;
    public int BPVRemaining;
    public bool InProgress;
}