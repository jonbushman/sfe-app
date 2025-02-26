using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using static DataClasses;

public class Player : MonoBehaviour
{
    public Floatilla Floatilla;
    public List<Colony> Colonies;
    public Dictionary<Location, Ledger> Wallet;
    public ErrorWindow ErrorWindow;

    public int TechLevel;

    private void Awake()
    {
        Floatilla = new Floatilla(ErrorWindow);
    }


    public void SavePlayerData()
    {
        //PlayerData data = new PlayerData();
        //data.Floatilla = Floatilla;
        //data.Wallet = Wallet;

        var floatilla = JsonConvert.SerializeObject(Floatilla.Ships);
        File.WriteAllText(Application.persistentDataPath + "/Floatilla.json", floatilla);




        // hex is a part of location, but is not serializable. what to do





    }
    public void LoadPlayerData()
    {

        Floatilla.Ships = JsonConvert.DeserializeObject<List<Ship>>(System.IO.File.ReadAllText(Application.persistentDataPath + "/Floatilla.json"));

        var fleetViewer = FindObjectOfType<FleetViewer>();
        fleetViewer.UpdateFleetView();
    }


}
