using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hangar : MonoBehaviour
{
    public Player Player;
    public Transform AllHexContainer;
    public GameObject HangarHexPrefab;
    public GameObject HangarBasePrefab;
    public GameObject HangarBayPrefab;

    public void Initialize()
    {
        var hexesWithHangars = new List<string>();
        foreach (var colony in Player.Colonies)
        {
            if (colony.Bases.Count > 0)
            {
                if (!hexesWithHangars.Contains(colony.HexID)) hexesWithHangars.Add(colony.HexID);
            }
        }

        for (var i = 0; i < hexesWithHangars.Count; i++)
        {
            var hexGO = Instantiate(HangarHexPrefab, AllHexContainer);
            var hex = hexGO.GetComponent<HangarHex>();

            foreach (var colony in Player.Colonies)
            {
                if (colony.HexID == hexesWithHangars[i] && colony.Bases.Count > 0)
                {
                    for (var j = 0; colony.Bases.Count > 0; j++)
                    {
                        var hangarBaseGO = Instantiate(HangarBasePrefab, hex.BasesContainer);
                        var hangarBaseInfo = colony.Bases[j];
                        var hangarBase = hangarBaseGO.GetComponent<HangarBase>();
                        hangarBase.Initialize();
                        for (var k = 0; k < hangarBaseInfo.BayNumber; k++)
                        {
                            var bayGO = Instantiate(HangarBayPrefab, hex.BaysContainer);
                            var bay = bayGO.GetComponent<HangarBay>();
                            bay.BaySize = hangarBaseInfo.BaySize;
                            bay.Initialize();
                        }
                    }
                }
            }
        }
    }


}
