using UnityEngine;
using System.IO;
using NUnit.Framework;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    [System.Serializable]
    public class MyData
    {
        public float Money;

        public int HighScore;

        public bool MachineGunLvl2;
        public bool MissileLauncher;
        public bool MissileLauncherLvl2;
        public bool LaserGun;
        public bool LaserGunLvl2;

        public bool MapLevel1Buyed;
        public bool MapLevel2Buyed;
        public bool MapLevel1Selected;
        public bool MapLevel2Selected;

        public string DifficultySelected; 
        public List<string> DifficultyList = new List<string> {"Easy" , "Easy+", "Normal", "Normal+", "Hard", "Hard+", "Insane", "Insane+", "Impossible", "Impossible+", "Hell", "Hell+", "Hell++", "Hell+++" };

        public List<string> DifficultyCompleted;
    }

    private static DataManager instance;
    private string filePath;

    private ErrorMessageDisplay errorMessageDisplay;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        filePath = Path.Combine(Application.persistentDataPath, "data.json");

        errorMessageDisplay = FindFirstObjectByType<ErrorMessageDisplay>();
    }

    public void SaveData(MyData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
    }

    public MyData LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            MyData data = JsonUtility.FromJson<MyData>(json);
            return data;
        }
        else
        {
            Debug.LogWarning("No saved data found.");
            ResetData();
            return null;
        }
    }

    public bool BuyMissileLauncher()
    {
        MyData data = LoadData();
        if (data != null && data.Money >= 300 && !data.MissileLauncher)
        {
            data.Money -= 300;
            data.MissileLauncher = true;
            SaveData(data);
            return true;
        }
        else if (data != null && data.Money < 300)
        {
            errorMessageDisplay.DisplayErrorMessage("Not enough money to buy Missile Launcher!");
            Debug.LogWarning("Not enough money to buy Missile Launcher.");
            return false;
        }
        else
        {
            errorMessageDisplay.DisplayErrorMessage("Unable to buy Missile Launcher!");
            Debug.LogWarning("Unable to buy Missile Launcher.");
            return false;
        }
    }

    public bool BuyLaserGun()
    {
        MyData data = LoadData();
        if (data != null && data.Money >= 600 && !data.LaserGun)
        {
            data.Money -= 600;
            data.LaserGun = true;
            SaveData(data);
            return true;
        }
        else if (data != null && data.Money < 600)
        {
            errorMessageDisplay.DisplayErrorMessage("Not enough money to buy Laser Gun!");
            Debug.LogWarning("Not enough money to buy Laser Gun.");
            return false;
        }
        else
        {
            errorMessageDisplay.DisplayErrorMessage("Unable to buy Laser Gun!");
            Debug.LogWarning("Unable to buy Laser Gun.");
            return false;
        }
    }

    public bool BuyMachineGunLvl2()
    {
        MyData data = LoadData();
        if (data != null && data.Money >= 100 && !data.MachineGunLvl2)
        {
            data.Money -= 100;
            data.MachineGunLvl2 = true;
            SaveData(data);
            return true;
        }
        else if (data != null && data.Money < 100)
        {
            errorMessageDisplay.DisplayErrorMessage("Not enough money to buy Machine Gun Lvl 2!");
            Debug.LogWarning("Not enough money to buy Machine Gun Lvl 2.");
            return false;
        }
        else
        {
            errorMessageDisplay.DisplayErrorMessage("Unable to buy Machine Gun Lvl 2!");
            Debug.LogWarning("Unable to buy Machine Gun Lvl 2.");
            return false;
        }
    }

    public bool BuyRocketLauncherLvl2()
    {
        MyData data = LoadData();
        if (data != null && data.Money >= 1200 && !data.MissileLauncherLvl2)
        {
            data.Money -= 1200;
            data.MissileLauncherLvl2 = true;
            SaveData(data);
            return true;
        }
        else if (data != null && data.Money < 1200)
        {
            errorMessageDisplay.DisplayErrorMessage("Not enough money to buy Machine Gun Lvl 2!");
            Debug.LogWarning("Not enough money to buy Machine Gun Lvl 2.");
            return false;
        }
        else
        {
            errorMessageDisplay.DisplayErrorMessage("Unable to buy Machine Gun Lvl 2!");
            Debug.LogWarning("Unable to buy Machine Gun Lvl 2.");
            return false;
        }
    }

    public bool UpgradeMap()
    {
        MyData data = LoadData();
        if (data != null && data.Money >= 600 && !data.MapLevel2Buyed)
        {
            data.Money -= 600;
            data.MapLevel2Buyed = true;
            SaveData(data);
            return true;
        }
        else if (data != null && data.Money < 100)
        {
            errorMessageDisplay.DisplayErrorMessage("Not enough money to upgrade map!");
            Debug.LogWarning("Not enough money to upgrade map.");
            return false;
        }
        else
        {
            errorMessageDisplay.DisplayErrorMessage("Unable to upgrade map!");
            Debug.LogWarning("Unable to upgrade map.");
            return false;
        }
    }

    public void ResetData()
    {
        MyData newData = new MyData();

        newData.Money = 0;
        newData.HighScore = 0;

        newData.MissileLauncher = false;
        newData.LaserGun = false;
        newData.MachineGunLvl2 = false;
        newData.MissileLauncherLvl2 = false;
        newData.LaserGunLvl2 = false;

        newData.MapLevel1Selected = true;
        newData.MapLevel1Buyed = true;

        newData.MapLevel2Selected = false;
        newData.MapLevel2Buyed = false;

        newData.DifficultyList = new List<string>();
        newData.DifficultyList.Add("Easy");
        newData.DifficultyList.Add("Easy+");
        newData.DifficultyList.Add("Normal");
        newData.DifficultyList.Add("Normal+");
        newData.DifficultyList.Add("Hard");
        newData.DifficultyList.Add("Hard+");
        newData.DifficultyList.Add("Insane");
        newData.DifficultyList.Add("Insane+");
        newData.DifficultyList.Add("Impossible");
        newData.DifficultyList.Add("Impossible+");
        newData.DifficultyList.Add("Hell");
        newData.DifficultyList.Add("Hell+");
        newData.DifficultyList.Add("Hell++");
        newData.DifficultyList.Add("Hell+++");

        newData.DifficultySelected = "Easy";

        newData.DifficultyCompleted = new List<string>();
        newData.DifficultyCompleted.Add("Easy");
        newData.DifficultyCompleted.Add("Easy+");

        SaveData(newData);
    }
}
