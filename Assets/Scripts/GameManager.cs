using Newtonsoft.Json;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player _player;
    public Map Map;

    private void Awake()
    {
        ShipDictionary.LoadFromJson();
        _player = GetComponent<Player>();
    }

    private void Start()
    {
    }


    public void SaveGame()
    {
        _player.SavePlayerData();

        //Application.persistentDataPath

        //var mapData = Map.SaveMapData();
        //string json = JsonConvert.SerializeObject(mapData, Formatting.Indented);

    }

    public void LoadGame()
    {
        _player.LoadPlayerData();



        //Map.LoadMapData();
    }

}
