using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipViewNameAndType : MonoBehaviour
{
    public string Name;
    public string Type;

    private ShipViewUI _shipViewUI;
    private Ship _ship;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _shipViewUI = GetComponentInParent<ShipViewUI>();
        _ship = _shipViewUI.Ship;

        Name = _ship.Name;
        Type = _ship.Type;

        SetText(Name, Type);
    }
    public void SetText(string name, string type)
    {
        var text = "<b><i>" + name + " - </b></i>";
        text += "<size=50%><voffset=.25em>" + type;
        _text.text = text;
    }



}
