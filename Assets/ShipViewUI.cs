using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipViewUI : MonoBehaviour
{
    //these get connected in the inspector
    public TMPro.TMP_Text TextName;
    public TMPro.TMP_Text TextType;

    //these get populated on creation
    public Ship Ship;




    public void Initialize(string name, string type, Ship ship)
    {
        TextName.text = name;
        TextType.text = type;
        Ship = ship;
    }
}
