using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    private void Awake()
    {
        if (instance != null)
            Debug.Log("More than one BuildManager in scene!");
        instance = this;
    }

    [Header("Preview Turrets")]
    public GameObject machineGunPreviewPrefab;
    public GameObject missileLauncherPreviewPrefab;
    public GameObject laserGunPreviewPrefab;

    [Header("Effect")]
    public GameObject buildEffect;

    public NodeUI nodeUI;

    private TurretBlueprint TurretToBuild;

    private Node selectedNode;

    public bool CanBuild { get { return TurretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.money >= TurretToBuild.cost; } }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        if (selectedNode != null)
        {
            selectedNode.turretComponent.HideRange();
        }

        selectedNode = node;
        TurretToBuild = null;

        nodeUI.SetTarget(node);
        node.turretComponent.ShowRange();
    }


    public void DeselectNode()
    {
        if (selectedNode == null)
            return;
        selectedNode.turretComponent.HideRange();
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        TurretToBuild = turret;
        
        DeselectNode();
    }


    public GameObject GetPreviewPrefab()
    {
        if (TurretToBuild == null)
            return null;

        if (TurretToBuild == Shop.instance.standardMachineGun)
            return machineGunPreviewPrefab;
        else if (TurretToBuild == Shop.instance.missileLauncher)
            return missileLauncherPreviewPrefab;
        else if (TurretToBuild == Shop.instance.laserGun)
            return laserGunPreviewPrefab;
        else
            return null;
    }

    public Vector3 GetPreviewOffset()
    {
        if (TurretToBuild == null)
            return Vector3.zero;

        if (TurretToBuild == Shop.instance.standardMachineGun)
            return Shop.instance.standardMachineGun.positionOffset + new Vector3(0, 0.5f, 0);
        else if (TurretToBuild == Shop.instance.missileLauncher)
            return Shop.instance.missileLauncher.positionOffset;
        else if (TurretToBuild == Shop.instance.laserGun)
            return Shop.instance.laserGun.positionOffset;
        else
            return Vector3.zero;
    }

    public TurretBlueprint GetTurretToBuild ()
    {
        return TurretToBuild;
    }
}
