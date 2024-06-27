using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FleetMovement : MonoBehaviour
{
    public TMP_Dropdown FleetContainer;
    public TMP_Dropdown SpeedContainer;
    public TMP_InputField StartingHex;
    public List<TMP_InputField> HexContainers;
    public Toggle VarySpeedContainer;
    public Toggle CargoContainer;

    private Map _map;

    private void OnEnable()
    {
        _map = FindObjectOfType<Map>();
        for (int i = 0; i < HexContainers.Count; i++)
        {
            var hexContainer = HexContainers[i].GetComponent<TMP_InputField>();
            var segment = i;
            hexContainer.onValueChanged.AddListener(CheckAdjacents);
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < HexContainers.Count; i++)
        {
            var hexContainer = HexContainers[i].GetComponent<TMP_InputField>();
            hexContainer.onValueChanged.RemoveAllListeners();
        }
    }

    private void CheckAdjacents(string arg0)
    {  
        for (int segment = 0; segment < HexContainers.Count; segment++)
        {
            var hexID = HexContainers[segment].text;
            if (hexID.Length != 4)
            {
                HighlightCell(segment, Color.red);
                continue;
            }
            var hexes = _map.GetAdjacentHexes(hexID);
        
            if (segment == 0)
            {
                if (!hexes.Contains(StartingHex.text)) HighlightCell(segment, Color.red);
                else HighlightCell(segment, Color.white);
            }
            else
            {
                if (!hexes.Contains(HexContainers[segment - 1].text)) HighlightCell(segment, Color.red);
                else HighlightCell(segment, Color.white);
            }
        }

    }

    private void HighlightCell(int segment, Color color)
    {
        var image = HexContainers[segment].GetComponent<Image>();
        image.color = color;
    }

    public void ReturnFromMap()
    {
        var hexes = _map.MovementOrders;

        if (hexes.Count > 0) StartingHex.text = hexes[0];
        else StartingHex.text = "";

        for (var i = 0; i < HexContainers.Count; i++)
        {
            if (i < hexes.Count - 1)
            {
                HexContainers[i].text = hexes[i+1];
            }
            else
            {
                HexContainers[i].text = "";
            }
        }
    }

    public void GoToMap()
    {
        _map.SelectingHexes = true;
        _map.MovementOrders.Clear();
        for (var i = 0;i < HexContainers.Count; i++)
        {
            //eventually should have error handling for bad hexes, since this is player input. but don't wanna do that rn
            if (HexContainers[i].text != "")
            {
                _map.MovementOrders.Add(HexContainers[i].text);
            }
        }
        _map.updateMovementLabels();
        _map.CurrentFleetMoving = this;
    }

}
