using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorWindow : MonoBehaviour
{
    public TMPro.TMP_Text TextBox;

    public void ValidationError(string message)
    {
        gameObject.SetActive(true);

        TextBox.text = message;    


    }

}
