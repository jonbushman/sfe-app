using System.Linq;
using TMPro;
using UnityEngine;

public class FleetViewer : MonoBehaviour
{
    public Player Player;
    public GameObject ShipViewPrefab;




    public void UpdateFleetView()
    {
        var fleet = Player.Floatilla;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Ship ship in fleet.Ships)
        {
            var shipView = Instantiate(ShipViewPrefab, transform);
            var shipViewUI = shipView.GetComponent<ShipViewUI>();
            shipViewUI.Ship = ship;
            shipViewUI.Initialize(ship.Name, ship.Type, ship);
        }
    }


    private void FleetAssignmentDropDown()
    {
        var allDropDowns = GetComponentsInChildren<TMP_Dropdown>().ToList();
        var dropDowns = allDropDowns.Find(x => x.gameObject.name == "Fleet Assignment");


    }
}
