using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static DataManager;

public class GameOver : MonoBehaviour
{
    public Text roundsText;
    public Text MoneyText;

    private float money;
    private DataManager dataManager;

    private void OnEnable()
    {
        roundsText.text = PlayerStats.round.ToString();
        dataManager = (DataManager)FindFirstObjectByType(typeof(DataManager));

        if (dataManager == null)
        {
            Debug.LogWarning("DataManager not found in the scene.");
            return;
        }

        MyData data = dataManager.LoadData();

        MoneyText.text = "Money: " + PlayerStats.round * 2 * DifficultyModifier.Multiplicator();

        money = data.Money + PlayerStats.round * 2 * DifficultyModifier.Multiplicator();
        
        if (data != null)
        {
            data.Money = money;
            dataManager.SaveData(data);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
