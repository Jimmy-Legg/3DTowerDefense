using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataManager;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    private DataManager dataManager;

    private Node target;

    public Button upgradeButton;

    [SerializeField]
    private Color ActiatedColor;
    [SerializeField]
    private Color DisabledColor;

    public GameObject MachineGunUpgradePrefab;
    public GameObject MissileLauncherUpgradePrefab;

    public void SetTarget(Node _target)
    {
        target = _target;

        Vector3 offset = new Vector3(0, 0.5f, 0);
        transform.position = target.GetBuildPosition(offset);

        ui.SetActive(true);
        dataManager = (DataManager)FindFirstObjectByType(typeof(DataManager));
        if (upgradeButton != null)
        {
            MyData myData = dataManager.LoadData();
            if (!myData.MachineGunLvl2 && _target.turretBlueprint.upgradedPrefab == MachineGunUpgradePrefab)
            {
                DisableButton();
                return;
            }else if (!myData.MissileLauncherLvl2 && _target.turretBlueprint.upgradedPrefab == MissileLauncherUpgradePrefab)
            {
                DisableButton();
                return;
            }
            if (PlayerStats.money < _target.turretBlueprint.upgradeCost)
            {
                DisableButton();
                return;
            }

            if (_target.turretBlueprint.upgradedPrefab == null)
            {
                DisableButton();
                return;
            }

            if (_target.isUpgraded)
            {
                DisableButton();
                return;
            }

            upgradeButton.enabled = true;
            upgradeButton.GetComponent<Image>().color = ActiatedColor;
        }
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        Manager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        Manager.instance.DeselectNode();
    }

    public void DisableButton()
    {
        upgradeButton.enabled = false;
        upgradeButton.GetComponent<Image>().color = DisabledColor;
    }

}
