//need to add a on-hover over map ui buttons, disable hex clicking

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int Width;
    public int Height;
    public float Buffer;
    public float HexSize;
    public bool CornerLowered = true;
    [Space(5)]
    [Header("Hex Prefabs")]
    public PrefabDictionary PrefabDictionary;

    public List<string> MovementOrders;
    public FleetMovement CurrentFleetMoving;
    public bool SelectingHexes = false;

    private List<List<Hex>> _hexList = new List<List<Hex>>();
    private int _prefabID;


    public void GenerateMap(bool randomHexPrefabs)
    {
        ClearMap();
        var size = HexSize + Buffer;
        int cornerAdjustment = 0;
        if (CornerLowered) cornerAdjustment = 1;

        List<HexData> data = new List<HexData>();

        if (!randomHexPrefabs)
        {
            data = LoadMapData();
        }

        for (int i = 0; i < Width; i++)
        {
            _hexList.Add(new List<Hex>());
            for (int j = 0; j < Height; j++)
            {
                var id = IndexToID(i,j);
                var hexType = new HexType();
                int prefabID;

                if (randomHexPrefabs)
                {
                    var hexTypeRoll = UnityEngine.Random.value;

                    float cumulativeProb = 0f;
                    foreach (var hT in PrefabDictionary)
                    {
                        if (!hT.Key.Equals(new HexType()))
                        {
                            cumulativeProb += hT.Key.Probability;
                            if ((hexTypeRoll < cumulativeProb) && (hexTypeRoll >= (cumulativeProb - hT.Key.Probability)))
                            {
                                hexType = hT.Key;
                            }
                        }
                    }
                    prefabID = ChooseRandomHexPrefab(hexType);
                }
                else
                {
                    int k = 0;
                    while (k < data.Count && data[k].ID != id) k++;

                    hexType = data[k].HexType;
                    prefabID = data[k].PrefabID;
                }

                Hex hex = Instantiate(PrefabDictionary[hexType].list[prefabID]);

                hex.transform.SetParent(transform, false);
                hex.name = id;
                hex.ID = id;
                hex.PrefabID = _prefabID;
                hex.HexType = hexType;
                _hexList[i].Add(hex);

                //adjusted for my own orientation sanity
                var position = Vector3.zero;
                position.x = i * size * 0.75f;
                position.z = -1 * size * (Mathf.Sqrt(3) / 2 * (j + ((i + cornerAdjustment) % 2) / 2f));
                hex.transform.localPosition = position;

                hex.LeftClicked.AddListener(() => onLeftClicked(id));
                hex.RightClicked.AddListener(() => onRightClicked(id));
            }
        }
    }

    #region Movement and Orders methods
    private void onLeftClicked(string id) 
    {
        if (MovementOrders.Count <= 6)
        {
            MovementOrders.Add(id);
            updateMovementLabels();
        }

    }
    private void onRightClicked(string id)
    {
        var index = IDTOIndex(id);
        var hex = _hexList[index[0]][index[1]];
        hex.HighlightEnable(false);
        hex.SegmentLabelEnable(false);

        while (MovementOrders.Contains(id)) MovementOrders.Remove(id);
        updateMovementLabels();
    }

    public void updateMovementLabels()
    {
        for(int i = 0; i < MovementOrders.Count; i++)
        {
            var index = IDTOIndex(MovementOrders[i]);
            var hex = _hexList[index[0]][index[1]];
            hex.HighlightEnable(true);
            hex.SegmentLabelEnable(true);
            hex.ChangeSegmentLabel(i);
        }
    }

    public void ReturnToOrders()
    {
        SelectingHexes = false;
        CurrentFleetMoving.ReturnFromMap();
    }
    #endregion

    public int ChooseRandomHexPrefab(HexType type)
    {
        int id = UnityEngine.Random.Range(0, PrefabDictionary[type].list.Count);

        return id;
    }

    public void ClearMap()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        _hexList = new List<List<Hex>>();
    }

    public List<HexData> SaveMapData()
    {
        var data = new List<HexData>();
        foreach (var row in _hexList)
        {
            foreach (var hex in row)
            {
                data.Add(hex.Save());
            }
        }

        return data;
    }

    public List<HexData> LoadMapData()
    {
        var serializer = new JsonSerializer();
        List<HexData> data = new();

        using (var streamReader = new StreamReader(@"Assets\Data\map.json"))
        using (var textReader = new JsonTextReader(streamReader))
        {
            data = serializer.Deserialize<List<HexData>>(textReader);
        }
        return data;
    }


    public void TempSaveToFile()
    {
        var data = SaveMapData();

        //string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        using (StreamWriter file = File.CreateText(@"Assets\Data\map.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, data);
        }
        //Debug.Log(json);
    }

    public void SmartSelection()
    {
        //needs to look at neighbors
        //should be recursive
        int recursions = 1;
    }

    private string IndexToID(int[] id)
    {
        return (id[0] + 1).ToString("00") + (id[1] + 1).ToString("00");
    }
    private string IndexToID(int i, int j)
    {
        return (i + 1).ToString("00") + (j + 1).ToString("00");
    }


    private int[] IDTOIndex(string id)
    {
        int i = Int32.Parse(id.Substring(0, 2)) - 1;
        int j = Int32.Parse(id.Substring(2, 2)) - 1;

        var idArray = new int[2];
        idArray[0] = i;
        idArray[1] = j;

        return idArray;
    }

    public List<string> GetAdjacentHexes(string hexID)
    {
        var x = int.Parse(hexID.Substring(0, 2));
        var y = int.Parse(hexID.Substring(2, 2));

        var up = ToTwoDigits(x);
        var right = ToTwoDigits(x+1);
        var rightDiagonal = ToTwoDigits(x+1);
        var down = ToTwoDigits(x);
        var left = ToTwoDigits(x-1);
        var leftDiagonal = ToTwoDigits(x-1);

        up += ToTwoDigits(y - 1);
        down += ToTwoDigits(y + 1);
        left += ToTwoDigits(y);
        right += ToTwoDigits(y);

        if (x%2 == 0)
        {
            leftDiagonal += ToTwoDigits(y + 1);
            rightDiagonal += ToTwoDigits(y + 1);
        }
        else
        {
            leftDiagonal += ToTwoDigits(y - 1);
            rightDiagonal += ToTwoDigits(y - 1);
        }

        //adding the appropriate hexes
        var adjacentHexes = new List<string>();
        adjacentHexes.Add(hexID);
        if (x == 1)
        {
            if (y == 1)
            {
                adjacentHexes.Add(right);
                adjacentHexes.Add(down);
            }
           else if (y == Height)
            {
                adjacentHexes.Add(right);
                adjacentHexes.Add(up);
                adjacentHexes.Add(rightDiagonal);
            }
            else
            {
                adjacentHexes.Add(up);
                adjacentHexes.Add(rightDiagonal);
                adjacentHexes.Add(right);
                adjacentHexes.Add(down);
            }
        }
        else if (x == Width)
        {
            if (y == 1)
            {
                adjacentHexes.Add(left);
                adjacentHexes.Add(down);
                if (Width % 2 == 0) adjacentHexes.Add(leftDiagonal);
            }
            else if (y == Height)
            {
                adjacentHexes.Add(left);
                adjacentHexes.Add(up);
                if (Width % 2 == 1) adjacentHexes.Add(leftDiagonal);
            }
            else
            {
                adjacentHexes.Add(up);
                adjacentHexes.Add(leftDiagonal);
                adjacentHexes.Add(left);
                adjacentHexes.Add(down);
            }
        }
        else
        {
            if (y == 1)
            {
                adjacentHexes.Add(left);
                adjacentHexes.Add(right);
                adjacentHexes.Add(down);
                if (y % 2 == 0)
                {
                    adjacentHexes.Add(leftDiagonal);
                    adjacentHexes.Add(rightDiagonal);
                }
            }
            else if (y == Height)
            {
                adjacentHexes.Add(left);
                adjacentHexes.Add(right);
                adjacentHexes.Add(up);
                if (y % 2 == 1)
                {
                    adjacentHexes.Add(leftDiagonal);
                    adjacentHexes.Add(rightDiagonal);
                }
            }
            else
            {
                //this is all middle hexes
                adjacentHexes.Add(left);
                adjacentHexes.Add(right);
                adjacentHexes.Add(up);
                adjacentHexes.Add(down);
                adjacentHexes.Add(leftDiagonal);
                adjacentHexes.Add(rightDiagonal);
            }
        }

        return adjacentHexes;
    }

    private string ToTwoDigits(int num)
    {
        if (num < 10)
        {
            return "0" + num.ToString();
        }
        else
        {
            return num.ToString();
        }
    }

    public void HexMovementSelection()
    {
        
    }
    
}

//neighbors of 003007:
//up 003008
//dn 003006
//lt 002006
//lt 002007
//rt 004006
//rt 004007


//neighbors of 008009:
//up 008008
//dn 008010
//lt 007009
//lt 007010
//rt 004006
//rt 004007


