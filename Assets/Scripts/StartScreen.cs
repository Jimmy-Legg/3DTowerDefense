using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static DataManager;
public class StartScreen : MonoBehaviour
{
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas upgradeCanvas;
    [SerializeField] private Canvas difficultyCanvas;
    [SerializeField] private DataManager dataManager;
    private MyData data;

    public Text Money;

    public void Start()
    {
        dataManager = (DataManager)FindFirstObjectByType(typeof(DataManager));
    }

    public void Update()
    {
        MyData data = dataManager.LoadData();

        if (data != null)
        {
            Money.text = "Money: " + data.Money;
        }
        else
        {
            Money.text = "Money: 0";
        }
    }

    public void StartGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void BackToMenu()
    {
        menuCanvas.gameObject.SetActive(true);
        upgradeCanvas.gameObject.SetActive(false);
        difficultyCanvas.gameObject.SetActive(false);
    }

    public void Upgrades()
    {
        menuCanvas.gameObject.SetActive(false);
        upgradeCanvas.gameObject.SetActive(true);
    }

    public void Difficulty()
    {
        menuCanvas.gameObject.SetActive(false);
        difficultyCanvas.gameObject.SetActive(true);
    }

    public void AddMoney()
    {
        data = dataManager.LoadData();
        data.Money = data.Money + 100;
        dataManager.SaveData(data);
    }
}
