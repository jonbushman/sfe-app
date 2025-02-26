using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AddShip : MonoBehaviour
{
    //have predictive placeholder text for ship name

    public Player Player;
    private Floatilla _floatilla;

    public string ShipTypeToAdd;
    public string ShipName;

    [SerializeField] UnityEvent OnClickEvents;

    public ErrorWindow ErrorWindow;

    public TMP_InputField ShipNameInput;

    private void Awake()
    {
        ShipTypeToAdd = "";
        ShipName = "";
    }

    private void Start()
    {
        _floatilla = Player.Floatilla;
    }

    public void OnClick()
    {
        ShipName = ShipNameInput.text;

        if (ShipTypeToAdd == "")
        {
            ErrorWindow.ValidationError("Please select a ship type");
            return;
        }
        if (ShipName == "")
        {
            ErrorWindow.ValidationError("Please enter a ship name");
            return;
        }

        var e = _floatilla.AddShip(ShipName, ShipTypeToAdd);
        if (e) return;



        OnClickEvents.Invoke();
    }

    public void UpdateShipType(string shipType)
    {
        ShipTypeToAdd = shipType;

        //can update placeholder text if nothing entered yet
    }




}
