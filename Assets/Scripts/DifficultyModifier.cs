using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static DataManager;

public class DifficultyModifier : MonoBehaviour { 
    public static DataManager dataManager;
    public static float Multiplicator()
    {
        dataManager = (DataManager)FindFirstObjectByType(typeof(DataManager));
        MyData data = dataManager.LoadData();
        // Retrieve selected difficulty and adjust enemy health accordingly
        switch (data.DifficultySelected)
        {
            case "Easy":
                return 1f;
            case "Easy+":
                return 2f;
            case "Normal":
                return 3f;
            case "Normal+":
                return 4f;
            case "Hard":
                return 5f;
            case "Hard+":
                return 6f;
            case "Insane":
                return 7f;
            case "Insane+":
                return 8f;
            case "Impossible":
                return 9f;
            case "Impossible+":
                return 10f;
            case "Hell":
                return 11f;
            case "Hell+":
                return 12f;
            case "Hell++":
                return 13f;
            case "Hell+++":
                return 14f;
            default:
                Debug.LogWarning("Unknown difficulty selected: " + data.DifficultySelected);
                return 1f;
        }
    }

}