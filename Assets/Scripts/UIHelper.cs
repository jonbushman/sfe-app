using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHelper : MonoBehaviour
{

    public TMP_InputField FirstHex;
    public TMP_InputField SecondHex;


    public void ToggleSpeeds(bool speed3)
    {
        var speedLabel = GetComponentInChildren<TMP_Text>();

        if (speed3)
        {
            SecondHex.interactable = false;
            SecondHex.text = FirstHex.text;
            speedLabel.text = "Speed 3";
        }
        else
        {
            SecondHex.interactable= true;
            speedLabel.text = "Speed 6";
        }

    }
}
