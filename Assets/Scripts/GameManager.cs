using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataManager;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver = false;
    public static bool GameIsWin = false;

    public GameObject gameOverUI;
    public GameObject gameWinUI;
    private DataManager dataManager;
    private MyData data;

    private void Start()
    {
        GameIsOver = false;
        GameIsWin = false;
        dataManager = (DataManager)FindFirstObjectByType(typeof(DataManager));
        data = dataManager.LoadData();
    }

    private void Update()
    {
        if (GameIsOver)
            return;

        if (PlayerStats.health <= 0)
        {
            EndGame();
        }
        if (WaveSpawner.waveIndex >= 30 && !GameIsWin && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            WinGame();

        }
    }

    private void WinGame()
    {
        int currentIndex = data.DifficultyList.IndexOf(data.DifficultySelected);

        Debug.Log("Current difficulty: " + data.DifficultySelected);
        Debug.Log("Difficulty list" + data.DifficultyList);
        if (currentIndex < data.DifficultyList.Count - 1)
        {
            string nextDifficulty = data.DifficultyList[currentIndex + 1];
            Debug.Log("Next difficulty: " + nextDifficulty);
            data.DifficultySelected = nextDifficulty;
            data.DifficultyCompleted.Add(nextDifficulty);
        }

        dataManager.SaveData(data);
        GameIsWin = true;
        gameWinUI.SetActive(true);
    }

    private void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }
}
