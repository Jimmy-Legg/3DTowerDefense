using UnityEngine;
using UnityEngine.UI;
using System.IO;
using static DataManager;

public class IsUpgraded : MonoBehaviour
{
    [SerializeField] private GameObject missileLauncherElement;
    [SerializeField] private GameObject laserGunElement;

    private DataManager dataManager;
    private MyData data;

    private void Start()
    {
        dataManager = (DataManager)FindFirstObjectByType(typeof(DataManager));

        if (dataManager != null)
        {
            if (dataManager != null)
            {
                data = dataManager.LoadData();

                if (data != null && data.MissileLauncher)
                {
                    missileLauncherElement.SetActive(true);
                }
                else
                {
                    missileLauncherElement.SetActive(false);
                }

                if (data != null && data.LaserGun)
                {
                    laserGunElement.SetActive(true);
                }
                else
                {
                    laserGunElement.SetActive(false);
                }
            }
            else
            {
                Debug.LogWarning("DataManager component not found on DataManager GameObject.");
            }
        }
        else
        {
            Debug.LogWarning("DataManager GameObject not found in the scene.");
        }
    }
}
