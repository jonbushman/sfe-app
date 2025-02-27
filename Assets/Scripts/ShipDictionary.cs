using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class ShipDictionary
{
    public static Dictionary<string, ShipTraits> AllShips;


    public static void PrintShipTraits(string shipName)
    {
        if (!AllShips.ContainsKey(shipName)) return;

        var str = "";
        foreach (PropertyInfo prop in AllShips[shipName].GetType().GetProperties())
        {
            str += prop.Name;
            str += ": ";
            str += prop.GetValue(AllShips[shipName]).ToString();
            str += "\n";
        }
    }

    public static void PrintAllShipTraits()
    {
        foreach (string ship in AllShips.Keys)
        {
            PrintShipTraits(AllShips[ship].ToString());
        }
    }

    public static void LoadFromJson()
    {
        JObject o1 = new JObject();
        AllShips = new Dictionary<string, ShipTraits>();
        
        using (StreamReader file = File.OpenText(Application.dataPath + "/Constants/ShipData.json"))
        using (JsonTextReader reader = new JsonTextReader(file))
        {
            o1 = (JObject)JToken.ReadFrom(reader);
        }


        foreach(var x in o1)
        {
            string ship = x.Key.ToString();

            AllShips[ship] = new ShipTraits();

            foreach (var y in x.Value)
            {
                var jsonLine = y.ToString().Split(":");
                var attr = jsonLine[0].Replace("\"",string.Empty);
                var val = jsonLine[1].Replace("\"", string.Empty).Trim();

                switch (attr)
                {
                    case "Type":
                        AllShips[ship].Type = val;
                        break;
                    case "Abbreviation":
                        AllShips[ship].Abbreviation = val;
                        break;
                    case "SubType":
                        AllShips[ship].SubType = val;
                        break;
                    case "ScanningStrength":
                        AllShips[ship].ScanningStrength = Int32.Parse(val);
                        break;
                    case "TechLevel":
                        AllShips[ship].TechLevel = Int32.Parse(val);
                        break;
                    case "Bpv":
                        AllShips[ship].Bpv = Int32.Parse(val);
                        break;
                    case "Size":
                        AllShips[ship].Size = Int32.Parse(val);
                        break;
                    case "Command":
                        AllShips[ship].Command = Int32.Parse(val);
                        break;
                    case "Attack":
                        AllShips[ship].Attack = Int32.Parse(val);
                        break;
                    case "Defense":
                        AllShips[ship].Defense = Int32.Parse(val);
                        break;
                    case "AttackCrippled":
                        AllShips[ship].AttackCripplped = Int32.Parse(val);
                        break;
                    case "DefenseCrippled":
                        AllShips[ship].DefenseCripplped = Int32.Parse(val);
                        break;
                    case "Cargo":
                        AllShips[ship].Cargo = Int32.Parse(val);
                        break;
                    case "Speed":
                        AllShips[ship].Speed = Int32.Parse(val);
                        break;
                    case "Fighters":
                        AllShips[ship].Fighters = Int32.Parse(val);
                        break;
                    case "Launch":
                        AllShips[ship].Launch = Int32.Parse(val);
                        break;
                    case "Bombard":
                        AllShips[ship].Bombard = Int32.Parse(val);
                        break;
                    case "Traits":
                        var l = new List<string>();
                        foreach(var v in val)
                        {
                            l.Add(v.ToString());
                        }

                        break;
                    default:
                        break;
                }


            }

        }




    }

}



public class ShipTraits
{
    public string Abbreviation;
    public string Type;
    public string SubType;
    public int TechLevel;
    public int Speed;
    public int Size;
    public int Bpv;
    public int Command;
    public int CommandCrippled;
    public int ScanningStrength;
    public int Attack;
    public int AttackCripplped;
    public int Defense;
    public int DefenseCripplped;
    public int Fighters;
    public int Launch;
    public int Bombard;
    public int Cargo;
    public List<string> Traits;
    public List<string> VersionHistory;

    public ShipTraits(string abbreviation, string type, string subType, int techLevel, int speed, int size, int bpv,
        int command, int commandCrippled, int scanningStrength, int attack, int attackCrippled, int defense, int defenseCrippled,
        int fighter, int launch, int bombard, int cargo, List<string> traits, List<string> versionHistory)
    {
        Abbreviation = abbreviation;
        Type = type;
        SubType = subType;
        TechLevel = techLevel;
        Speed = speed;
        Size = size;
        Bpv = bpv;
        Command = command;
        CommandCrippled = commandCrippled;
        ScanningStrength = scanningStrength;
        Attack = attack;
        AttackCripplped = attackCrippled;
        Defense = defense;
        DefenseCripplped = defenseCrippled;
        Fighters = fighter;
        Launch = launch;
        Bombard = bombard;
        Cargo = cargo;
        Traits = traits;
        VersionHistory = versionHistory;
    }

    public ShipTraits()
    {

    }
}