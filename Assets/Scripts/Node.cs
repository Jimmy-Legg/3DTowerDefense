using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static DataManager;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color problemColor;

    [HideInInspector]
    public GameObject turret;
    public Turret turretComponent;

    [HideInInspector]
    public TurretBlueprint turretBlueprint;

    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    private Manager manager;

    private GameObject previewPrefab;
    private Turret previewTurretComponent;

    private float lastTapTime;
    private const float doubleTapTimeThreshold = 0.2f;

    private ErrorMessageDisplay errorMessageDisplay;


    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        manager = Manager.instance;
        errorMessageDisplay = FindFirstObjectByType<ErrorMessageDisplay>();
    }

    public Vector3 GetBuildPosition(Vector3 offset)
    {
        return transform.position + offset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            manager.SelectNode(this);
        }

        if (!manager.CanBuild)
            return;

        float timeSinceLastTap = Time.time - lastTapTime;
        if (timeSinceLastTap < doubleTapTimeThreshold)
        {
            BuildTurret(manager.GetTurretToBuild());
            if (turret != null)
            {
                turretComponent = turret.GetComponent<Turret>();
            }
        }
        else
        {
            lastTapTime = Time.time;
        }
    }

    private void BuildTurret(TurretBlueprint blueprint)
    {
        if (previewPrefab != null)
        {
            Destroy(GameObject.Find(previewPrefab.name + "(Clone)"));
        }
        if (blueprint == null)
        {
            errorMessageDisplay.DisplayErrorMessage("No turret selected to build!");
            Debug.Log("No turret selected to build!");
            return;
        }

        if (PlayerStats.money < blueprint.cost)
        {
            errorMessageDisplay.DisplayErrorMessage("Not enough money to build that!");
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(blueprint.positionOffset), Quaternion.identity);
        turret = _turret;

        if (previewPrefab != null)
        {
            previewTurretComponent = _turret.GetComponent<Turret>();
        }

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(manager.buildEffect, GetBuildPosition(blueprint.positionOffset), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void UpgradeTurret()
    {
        DataManager dataManager = (DataManager)FindFirstObjectByType(typeof(DataManager));
        if (dataManager != null) {
            
            if (PlayerStats.money < turretBlueprint.upgradeCost)
            {

                errorMessageDisplay.DisplayErrorMessage("Not enough money to upgrade");
                Debug.Log("Not enough money to build that!");
                return;
            }

            if (turretBlueprint.upgradedPrefab == null)
            {
                errorMessageDisplay.DisplayErrorMessage("No upgraded prefab set for this turret!");
                Debug.Log("No upgraded prefab set for this turret!");
                return;
            }

            if (isUpgraded)
            {
                errorMessageDisplay.DisplayErrorMessage("Turret already upgraded!");
                Debug.Log("Turret already upgraded!");
                return;
            }
            
            

            PlayerStats.money -= turretBlueprint.upgradeCost;

            Destroy(turret);

            GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(turretBlueprint.positionOffset), Quaternion.identity);
            turret = _turret;

            GameObject effect = (GameObject)Instantiate(manager.buildEffect, GetBuildPosition(turretBlueprint.positionOffset), Quaternion.identity);
            Destroy(effect, 5f);

            if (turret != null)
            {
                turretComponent.HideRange();
            }

            isUpgraded = true;

            Debug.Log("Turret upgraded!");
        }
    }

    public void SellTurret()
    {
        PlayerStats.money += turret.GetComponent<Turret>().sellPrice;

        GameObject effect = (GameObject)Instantiate(manager.buildEffect, GetBuildPosition(turretBlueprint.positionOffset), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);
        turretBlueprint = null;
        isUpgraded = false;

        if (turretComponent != null)
        {
            turretComponent.HideRange();
        }
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!manager.CanBuild)
            return;

        if (!manager.HasMoney || turret != null)
        {
            rend.material.color = problemColor;
        }
        else
        {
            rend.material.color = hoverColor;

            previewPrefab = manager.GetPreviewPrefab();
            if (previewPrefab != null)
            {
                Vector3 offset = manager.GetPreviewOffset();
                Instantiate(previewPrefab, GetBuildPosition(offset) + new Vector3(0, 0.1f, 0), Quaternion.identity);

                if (previewTurretComponent != null)
                {
                    previewTurretComponent.ShowRange();
                }
            }
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;

        if (previewPrefab != null)
        {
            Destroy(GameObject.Find(previewPrefab.name + "(Clone)"));
        }

        if (previewTurretComponent != null)
        {
            previewTurretComponent.HideRange();
        }
    }
}
