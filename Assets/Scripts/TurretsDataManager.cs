using UnityEngine;
using System.IO;
using System;
using UnityEditor;

public class TurretsDataManager : MonoBehaviour
{
    [System.Serializable]
    public class MyData
    {
        // Fire rate upgrades for Machine Gun
        public float[] fireRateUpgradesMachineGunLvl1 = {0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f, 1.2f, 1.4f };
        public float lastMachineGunLvl1FireRateBuyed;
        public float[] fireRateUpgradesMachineGunLvl2 = {0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f, 1.2f, 1.4f, 1.6f, 1.8f, 2.0f  };
        public float lastMachineGunLvl2FireRateBuyed;

        // Fire rate upgrades for Missile Launcher
        public float[] fireRateUpgradesMissileLauncherLvl1 = {0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f };
        public float lastMissileLauncherLvl1FireRateBuyed;
        public float[] fireRateUpgradesMissileLauncherLvl2 = {0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f };
        public float lastMissileLauncherLvl2FireRateBuyed;

        // Fire rate upgrades for Laser Gun
        public float[] fireRateUpgradesLaserGunLvl1 = {0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f };
        public float lastLaserGunLvl1FireRateBuyed;
        public float[] fireRateUpgradesLaserGunLvl2 = {0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f };
        public float lastLaserGunLvl2FireRateBuyed;
    }

    private static TurretsDataManager instance;
    private string filePath;

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

        filePath = Path.Combine(Application.persistentDataPath, "turretData.json");
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

    public void UpgradeTurret(string turretType, int currentLevel, int moneySpent)
    {
        DataManager dataManager = (DataManager)FindFirstObjectByType(typeof(DataManager));
        DataManager.MyData dataMoney = dataManager.LoadData();
        MyData data = LoadData();

        // Determine the float array and the last buyed fire rate based on the turret type and current level
        float[] fireRateArray;
        ref float lastFireRateBuyed = ref data.lastMachineGunLvl1FireRateBuyed;

        switch (turretType)
        {
            case "MachineGun":
                if (currentLevel == 2)
                {
                    fireRateArray = data.fireRateUpgradesMachineGunLvl2;
                    lastFireRateBuyed = ref data.lastMachineGunLvl2FireRateBuyed;
                }
                else
                {
                    fireRateArray = data.fireRateUpgradesMachineGunLvl1;
                    lastFireRateBuyed = ref data.lastMachineGunLvl1FireRateBuyed;
                }
                break;
            case "MissileLauncher":
                if (currentLevel == 2)
                {
                    fireRateArray = data.fireRateUpgradesMissileLauncherLvl2;
                    lastFireRateBuyed = ref data.lastMissileLauncherLvl2FireRateBuyed;
                }
                else
                {
                    fireRateArray = data.fireRateUpgradesMissileLauncherLvl1;
                    lastFireRateBuyed = ref data.lastMissileLauncherLvl1FireRateBuyed;
                }
                break;
            case "LaserGun":
                if (currentLevel == 2)
                {
                    fireRateArray = data.fireRateUpgradesLaserGunLvl2;
                    lastFireRateBuyed = ref data.lastLaserGunLvl2FireRateBuyed;
                }
                else
                {
                    fireRateArray = data.fireRateUpgradesLaserGunLvl1;
                    lastFireRateBuyed = ref data.lastLaserGunLvl1FireRateBuyed;
                }
                break;
            default:
                Debug.LogWarning("Invalid turret type: " + turretType);
                return;
        }

        // Find the index of the last fire rate buyed in the array
        int lastIndex = Array.IndexOf(fireRateArray, lastFireRateBuyed);

        // Check if there's an upgrade available
        if (lastIndex < fireRateArray.Length - 1)
        {
            if (dataManager != null)
            {
                if (dataMoney.Money >= moneySpent)
                {
                    dataMoney.Money -= moneySpent;
                    lastFireRateBuyed = fireRateArray[lastIndex + 1];

                    // Save the updated data
                    dataManager.SaveData(dataMoney);
                    SaveData(data);
                }
                else
                {
                    Debug.LogWarning("Not enough money to buy the upgrade.");
                    return;
                }
            }
        }
        else
        {
            Debug.LogWarning("Turret is already at maximum upgrade level.");
        }
    }


    public void ResetData()
    {
        MyData newData = new MyData();

        newData.lastMachineGunLvl1FireRateBuyed = newData.fireRateUpgradesMachineGunLvl1[0];
        newData.lastMachineGunLvl2FireRateBuyed = newData.fireRateUpgradesMachineGunLvl2[0];

        newData.lastMissileLauncherLvl1FireRateBuyed = newData.fireRateUpgradesMissileLauncherLvl1[0];
        newData.lastMissileLauncherLvl2FireRateBuyed = newData.fireRateUpgradesMissileLauncherLvl2[0];

        newData.lastLaserGunLvl1FireRateBuyed = newData.fireRateUpgradesLaserGunLvl1[0];
        newData.lastLaserGunLvl2FireRateBuyed = newData.fireRateUpgradesLaserGunLvl2[0];

        SaveData(newData);
    }
}
