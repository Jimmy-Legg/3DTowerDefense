using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataManager;

public class MapManager : MonoBehaviour
{
    public static bool MapPlaced;
    [SerializeField] private GameObject mapLvl0Prefab;
    [SerializeField] private GameObject mapLvl1Prefab;
    
    private DataManager dataManager;

    private void Start()
    {
        MapPlaced = false;
        GameObject dataManagerObject = GameObject.Find("DataManager");
        dataManager = (DataManager)FindFirstObjectByType(typeof(DataManager));
        MyData data = dataManager.LoadData();

        if (data != null)
        {
            Debug.Log(data.MapLevel1Selected);
            if (data.MapLevel1Selected)
            {
                Instantiate(mapLvl0Prefab, Vector3.zero, Quaternion.identity);
                MapPlaced = true;
            }
            else if (data.MapLevel2Selected)
            {
                Instantiate(mapLvl1Prefab, Vector3.zero, Quaternion.identity);
                MapPlaced = true;
            }
        }
        else
        {
            Debug.LogWarning("No data found.");
        }
    }
}
