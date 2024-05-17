using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static DataManager;

public class UpgradeShop : MonoBehaviour
{
    public DataManager dataManager;
    private DataManager.MyData data;

    public TurretsDataManager turretDataManager;
    private TurretsDataManager.MyData turretData;

    [Header("Money")]
    [SerializeField] private Text moneyText;

    [Header("Buy Buttons")]
    public Button MachineGunLvl2BuyButton;
    public Button MissileLauncherLvl1BuyButton;
    public Button MissileLauncherLvl2BuyButton;
    public Button LaserGunLvl1BuyButton;

    [Header("Slider MachinGun")]
    public Slider machineGunLvl1Slider;
    public Button machineGunLvl1BuyButton;
    public Slider machineGunLvl2Slider;
    public Button machineGunLvl2BuyButton;

    [Header("Slider MissileLauncher")]
    public Slider missileLauncherLvl1Slider;
    public Button missileLauncherLvl1BuyButton;
    public Slider missileLauncherLvl2Slider;
    public Button missileLauncherLvl2BuyButton;

    [Header("Slider LaserGun")]
    public Slider laserGunLvl1Slider;
    public Button laserGunLvl1BuyButton;

    [Header("Map Buttons")]
    public Button MapButtonlvl1;
    public Button MapButtonlvl2;


    [Header("Price Tooltip")]
    public GameObject priceTooltip;
    public TextMeshProUGUI priceText;

    public void Start()
    {
        dataManager = (DataManager)FindFirstObjectByType(typeof(DataManager));
        turretDataManager = (TurretsDataManager)FindFirstObjectByType(typeof(TurretsDataManager));

        moneyText.text = "Money: " + dataManager.LoadData().Money.ToString();

        data = dataManager.LoadData();
        turretData = turretDataManager.LoadData();

        InitializeSliders();

        priceTooltip.SetActive(false);


        //if turret is buyed in data then change button text to selected
        if (data.MissileLauncher)
        {
            IsBuyed(MissileLauncherLvl1BuyButton);
        }

        if(data.LaserGun)
        {
            IsBuyed(LaserGunLvl1BuyButton);
        }

        if (data.MachineGunLvl2)
        {
            IsBuyed(MachineGunLvl2BuyButton);
        }

        if (data.MissileLauncherLvl2)
        {
            IsBuyed(MissileLauncherLvl2BuyButton);
        }

        // select map
        if (data.MapLevel1Selected)
        {
            SelectMapLvl1();
        }
        else if (data.MapLevel2Selected)
        {
            SelectMapLvl2();
        }

    }

    public void ShowPriceTooltip(string price)
    {
        priceText.text = "Price: $" + price;
        priceTooltip.SetActive(true);
    }

    public void HidePriceTooltip()
    {
        priceTooltip.SetActive(false);
    }

    private void InitializeSliders()
    {
        turretData = turretDataManager.LoadData();
        machineGunLvl1Slider.maxValue = turretData.fireRateUpgradesMachineGunLvl1[turretData.fireRateUpgradesMachineGunLvl1.Length - 1];
        machineGunLvl1Slider.value = turretData.lastMachineGunLvl1FireRateBuyed;

        machineGunLvl2Slider.maxValue = turretData.fireRateUpgradesMachineGunLvl2[turretData.fireRateUpgradesMachineGunLvl2.Length - 1];
        machineGunLvl2Slider.value = turretData.lastMachineGunLvl2FireRateBuyed;

        missileLauncherLvl1Slider.maxValue = turretData.fireRateUpgradesMissileLauncherLvl1[turretData.fireRateUpgradesMissileLauncherLvl1.Length - 1];
        missileLauncherLvl1Slider.value = turretData.lastMissileLauncherLvl1FireRateBuyed;

        missileLauncherLvl2Slider.maxValue = turretData.fireRateUpgradesMissileLauncherLvl2[turretData.fireRateUpgradesMissileLauncherLvl2.Length - 1];
        missileLauncherLvl2Slider.value = turretData.lastMissileLauncherLvl2FireRateBuyed;

        laserGunLvl1Slider.maxValue = turretData.fireRateUpgradesLaserGunLvl1[turretData.fireRateUpgradesLaserGunLvl1.Length - 1];
        laserGunLvl1Slider.value = turretData.lastLaserGunLvl1FireRateBuyed;
        MyData data = dataManager.LoadData();
        if (data.MissileLauncher)
        {
            IsBuyed(MissileLauncherLvl1BuyButton);
        }

        if (data.LaserGun)
        {
            IsBuyed(LaserGunLvl1BuyButton);
        }

        if (data.MachineGunLvl2)
        {
            IsBuyed(MachineGunLvl2BuyButton);
        }

        if (data.MissileLauncherLvl2)
        {
            IsBuyed(MissileLauncherLvl2BuyButton);
        }
    }

    public void BuyMachineGunLvl2()
    {
        bool isBuyed = dataManager.BuyMachineGunLvl2();
        if (isBuyed || data.MachineGunLvl2)
        {
            IsBuyed(MachineGunLvl2BuyButton);
        }
    }

    public void BuyMissileLauncher()
    {
        bool isBuyed = dataManager.BuyMissileLauncher();
        if (isBuyed || data.MissileLauncher)
        {
            IsBuyed(MissileLauncherLvl1BuyButton);
        }
    }
    public void BuyMissileLauncherLvl2()
    {
        bool isBuyed = dataManager.BuyRocketLauncherLvl2();
        if (isBuyed || data.MissileLauncherLvl2)
        {
            IsBuyed(MachineGunLvl2BuyButton);
        }
    }
    public void BuyLaserGun()
    {
        bool isBuyed = dataManager.BuyLaserGun();
        if (isBuyed || data.LaserGun)
        {
            IsBuyed(LaserGunLvl1BuyButton);
        }
    }

    public void UpgradeMapLvl2()
    {
        if(data.MapLevel2Buyed)
            return;
        bool isBuyed = dataManager.UpgradeMap();
        moneyText.text = "Money: " + dataManager.LoadData().Money.ToString();
        data = dataManager.LoadData();
        if (isBuyed || data.MapLevel2Buyed)
        {
            IsBuyed(MapButtonlvl2);
        }
    }

    public void SelectMapLvl1() 
    { 
        if (data.MapLevel2Buyed)
        {
            TextMeshProUGUI buttonTextlvl1 = MapButtonlvl1.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI buttonTextlvl2 = MapButtonlvl2.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonTextlvl2 != null && buttonTextlvl1 != null)
            {
                buttonTextlvl1.text = "Selected";
                buttonTextlvl2.text = "Select";
            }
            data.MapLevel2Selected = false;
            data.MapLevel1Selected = true;

            dataManager.SaveData(data);

        }
    }

    public void SelectMapLvl2()
    {
        if (data.MapLevel2Buyed)
        {
            TextMeshProUGUI buttonTextlvl1 = MapButtonlvl1.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI buttonTextlvl2 = MapButtonlvl2.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonTextlvl2 != null && buttonTextlvl1 != null)
            {
                buttonTextlvl1.text = "Select";
                buttonTextlvl2.text = "Selected";
            }
            data.MapLevel2Selected = true;
            data.MapLevel1Selected = false;
            Debug.Log("non non non");
            dataManager.SaveData(data);

        }
    }
    public void UpgradeMachineGunLvl1()
    {
        int moneySpent = 50;

        turretDataManager.UpgradeTurret("MachineGun", 1, moneySpent);
        
        UpdateSliders();
    }

    public void UpgradeMachineGunLvl2()
    {
        if (!data.MachineGunLvl2)
            return;
        int moneySpent = 100;

        turretDataManager.UpgradeTurret("MachineGun", 2, moneySpent);
        
        UpdateSliders();
    }

    public void UpgradeMissileLauncher()
    {
        if (!data.MissileLauncher)
            return;
        int moneySpent = 300;

        turretDataManager.UpgradeTurret("MissileLauncher", 1, moneySpent);

        UpdateSliders();

    }

    public void UpgradeMissileLauncherLvl2()
    {
        if (!data.MissileLauncherLvl2)
            return;
        int moneySpent = 600;

        turretDataManager.UpgradeTurret("MissileLauncher", 2, moneySpent);

        UpdateSliders();
    }

    public void UpgradeLaserGun()
    {
        if (!data.LaserGun)
            return;
        int moneySpent = 600;

        turretDataManager.UpgradeTurret("LaserGun", 1, moneySpent);

        UpdateSliders();
    }

    public void Update()
    {
        moneyText.text = "Money: " + dataManager.LoadData().Money.ToString();
        data = dataManager.LoadData();
        InitializeSliders();
    }

    private void UpdateSliders()
    {
        turretData = turretDataManager.LoadData();

        machineGunLvl1Slider.value = turretData.lastMachineGunLvl1FireRateBuyed;

        machineGunLvl2Slider.value = turretData.lastMachineGunLvl2FireRateBuyed;

        missileLauncherLvl1Slider.value = turretData.lastMissileLauncherLvl1FireRateBuyed;

        missileLauncherLvl2Slider.value = turretData.lastMissileLauncherLvl2FireRateBuyed;

        laserGunLvl1Slider.value = turretData.lastLaserGunLvl1FireRateBuyed;
    }

    public void ResetData()
    {
        dataManager.ResetData();
    }

    public void ResetDataTurret()
    {
        turretDataManager.ResetData();
    }

    public void IsBuyed(Button button)
    {
        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = "Buyed";
        }
        else
        {
            Debug.LogWarning("Text component not found as a child of the button.");
        }
    }
}
