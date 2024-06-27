using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using UnityEditor.ShaderKeywordFilter;

public class ShipListUI : MonoBehaviour
{
    private TMPro.TMP_Dropdown _dropdown;
    private TMPro.TMP_Text _text;

    public GameObject ShipSelectionPrefab;
    public Transform ShipSelectionViewer;



    void Start()
    {
        foreach(Transform child in ShipSelectionViewer.transform)
        {
            Destroy(child.gameObject);
        }


        var techLevels = new List<int>();
        var sortedList = new List<string>();

        foreach (var ship in ShipDictionary.AllShips)
        {
            sortedList.Add(ship.Key);
            var tL = ship.Value.TechLevel;
            if (!techLevels.Contains(tL))
            {
                techLevels.Add(tL);
            }
        }
        techLevels.Sort();
        sortedList.Sort();


        foreach (var tL in techLevels)
        {
            var selection = Instantiate(ShipSelectionPrefab, ShipSelectionViewer);
            var shipSelectionScript = selection.GetComponent<ShipSelectionUI>();

            var text = "--- Tech Level " + tL.ToString() + " ---";
            selection.name = text;
            shipSelectionScript.Bind(text);

            var button = selection.GetComponentInChildren<Button>();
            button.interactable = false;

            //button.image.color = new Color(2, 99, 202);

            //var colorBlock = new ColorBlock()
            //{
            //    normalColor = new Color(2, 99, 202),
            //    highlightedColor = new Color(2, 99, 202),
            //    pressedColor = new Color(2, 99, 202),
            //    selectedColor = new Color(2, 99, 202),
            //    disabledColor = new Color(2, 99, 202),
            //    colorMultiplier = 1
            //};
            ColorBlock colorBlock = button.colors;
            colorBlock.disabledColor = new Color(2f/255, 99f/255, 202f/255);
            button.colors = colorBlock; //damn, still not working

            foreach (var ship in sortedList)
            {
                if (tL == ShipDictionary.AllShips[ship].TechLevel)
                {

                    selection = Instantiate(ShipSelectionPrefab, ShipSelectionViewer);
                    shipSelectionScript = selection.GetComponent<ShipSelectionUI>();
                    selection.name = "Sel_" + ship;
                    shipSelectionScript.Bind(ship);
                    shipSelectionScript.OnShipSelected.AddListener(OnShipSelected);
                }

            }

        }

    }

    private void OnShipSelected(ShipSelectionUI arg0)
    {
        Debug.Log(arg0.LinkedText);
    }

    void Update()
    {
        
    }

}
