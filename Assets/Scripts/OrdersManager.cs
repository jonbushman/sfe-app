using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersManager
{
    public void AddShipToFleet(Ship ship, Floatilla fleet) { }
    public void DestroyShip(Ship ship) { }
    public void RemoveShipFromFleet(Ship ship) { }
    public void AddShipToSquadron(Ship ship, Squadron squadron) { }
    public void RemoveShipFromSquadron(Ship ship) { }
    public void SetShipTraits(Ship ship) { }
    public void ModifyShipTraits(Ship ship, ShipTraits newTraits) { }

    public void AddSquadronMovement(Squadron squadron, List<Hex> path)
    {
        foreach (Ship ship in squadron.Ships)
        {
            AddShipMovement(ship, path);
        }
    }
    private void AddShipMovement(Ship ship, List<Hex> path) { }

    public void BeginShipConstruction(Ship ship, int year, int segment) { }
    public void PauseShipConstruction(Ship ship, int year, int segment) { }
    public void ResumeShipConstruction(Ship ship, int year, int segment) { }



}



