

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Hex : MonoBehaviour
{
    public string ID;
    public int PrefabID;
    public HexType HexType;
    public UnityEvent LeftClicked;
    public UnityEvent RightClicked;

    public TMP_Text SegmentLabel;
    public GameObject Highlighter;

    private Map _map;

    private void OnEnable()
    {
        _map = FindObjectOfType<Map>();
    }

    private void OnMouseOver()
    {
        if (_map.SelectingHexes == false) return;   
        if (Input.GetMouseButtonDown(0))
        {
            LeftClicked?.Invoke();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            RightClicked?.Invoke();
        }
    }
    public void ChangeSegmentLabel(int segment) //this should not be a thing. should spawn in an a new TMP at this same position , but not have it be directly tied to the hex
    {
        SegmentLabel.text = segment.ToString();
    }

    public void HighlightEnable(bool x)
    {
        Highlighter.SetActive(x);
    }

    public void SegmentLabelEnable(bool x)
    {
        SegmentLabel.gameObject.SetActive(x);
    }

    public void AddColonizable(Colonizable col)
    {
        if (HexType.Colonizable1 == Colonizable.None)
        {
            HexType.Colonizable1 = col;
        }
        else if (HexType.Colonizable2 == Colonizable.None)
        {
            HexType.Colonizable2 = col;
        }
        else if (HexType.Colonizable3 == Colonizable.None)
        {
            HexType.Colonizable3 = col;
        }
        else
        {
            Debug.Log("bad");
        }

        UpdateMaterial();
    }

    public void AddAnomaly(Anomaly anom)
    {
        if (HexType.Anomaly1 == Anomaly.None)
        {
            HexType.Anomaly1 = anom;
        }
        else if (HexType.Anomaly2 == Anomaly.None)
        {
            HexType.Anomaly2 = anom;
        }
        else if (HexType.Anomaly3 == Anomaly.None)
        {
            HexType.Anomaly3 = anom;
        }
        else
        {
            Debug.Log("bad");
        }
        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        //need some kind of prioritization here to decide what actually gets shown
        //or maybe have everything be shown, but gets re-drawn when user wants to filter
    }


    public HexData Save()
    {
        var data = new HexData();
        data.ID = ID;
        data.PrefabID = PrefabID;
        data.HexType = HexType;

        return data;
    }

    public void Load(HexData data)
    {
        ID = data.ID;
        PrefabID = data.PrefabID;
        HexType = data.HexType;
    }
}

[Serializable]
public class HexData
{
    public string ID;
    public int PrefabID;
    public HexType HexType;

    public HexData() { }
}
[Serializable]
public struct HexType
{
    public Colonizable Colonizable1;
    public Colonizable Colonizable2;
    public Colonizable Colonizable3;
    public Anomaly Anomaly1;
    public Anomaly Anomaly2;
    public Anomaly Anomaly3;
    public float Probability;
}

[Serializable]
public enum Anomaly
{
    None,
    Blackhole_Major,
    Blackhole_Minor,
    Nebula,
    Ion_Storm,
}
[Serializable]
public enum Colonizable
{
    None,
    Terran,
    Jungle,
    Ocean,
    Barren,
    Moon,
    Asteroid_Belt,
}