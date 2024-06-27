using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }
    public void DuplicateShip(int generations)
    {
        var t = transform;
        for (int i = 0; i < generations; i++)
        {
            t = t.parent;
        }

        //var newShipView = Instantiate(t.gameObject, t.transform.parent);

        //var ship = newShipView.GetComponent<ShipViewUI>().Ship;
        //ship.Name = ship.Name + " Copy";

        var shipView = t.GetComponent<ShipViewUI>();
        var originalShip = shipView.Ship;
        
        string newName = originalShip.Name + " Copy";
        var ship = new Ship(newName, originalShip.Traits.Type);
        ship.Traits = originalShip.Traits;

        _player.Floatilla.AddShip(ship, newName);

        var fleetViewer = t.transform.GetComponentInParent<FleetViewer>();
        fleetViewer.UpdateFleetView();
    }
}
