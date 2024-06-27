
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavigationGroup : MonoBehaviour
{
    [SerializeField] private List<TMP_InputField> _inputFields;
    [SerializeField] private TMP_InputField _currentlySelected;
    [SerializeField] private bool _wrap;

    [SerializeField] private EventSystem _eventSystem;

    //private void OnEnable()
    //{
    //    var _eventSystem = FindObjectOfType<EventSystem>();
    //}

    private void SelectInput(string arg0)
    {
        throw new NotImplementedException();
    }

    void Update()
    {
        var selected = _eventSystem.currentSelectedGameObject;
        if (!selected) return;
        var selectedInput = selected.GetComponent<TMP_InputField>();
        if (!selectedInput) return;

        if (_inputFields.Contains(selectedInput))
        {
            _currentlySelected = selectedInput;
            if (Input.GetKeyUp(KeyCode.RightArrow) || (Input.GetKeyUp(KeyCode.Tab) && !Input.GetKey(KeyCode.LeftShift)))
            {
                ChangeSelection(1);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
            {
                ChangeSelection(-1);
            }
        }    
    }

    private void ChangeSelection(int move)
    {
        var index = _inputFields.IndexOf(_currentlySelected);
        index += move;
        if (index < 0)
        {
            if (_wrap) index = _inputFields.Count - 1;
            else index = 0;
        }
        else if (index >= _inputFields.Count)
        {
            if (_wrap) index = 0;
            else index = _inputFields.Count - 1;
        }
        _inputFields[index].Select();
    }

}
