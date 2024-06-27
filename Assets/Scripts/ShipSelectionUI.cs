using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ShipSelectedEvent : UnityEvent<ShipSelectionUI>{}

public class ShipSelectionUI : MonoBehaviour
{

    public ShipSelectedEvent OnShipSelected = new ShipSelectedEvent();
    [SerializeField] public TextMeshProUGUI LinkedText;

    AddShip _addShipBtn;

    void Start()
    {
        _addShipBtn = FindObjectOfType<AddShip>();
    }

    public void Bind(string text)
    {
        LinkedText.text = text;
    }

    void Update()
    {
        
    }

    public void OnSelected()
    {
        OnShipSelected.Invoke(this);
        var textObj = GetComponentInChildren<TextMeshProUGUI>();
        _addShipBtn.UpdateShipType(textObj.text);
    }
}
