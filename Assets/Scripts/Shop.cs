using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    public TurretBlueprint standardMachineGun;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserGun;

    private void Awake()
    {
        if (instance != null)
            Debug.Log("More than one Shop in scene!");
        instance = this;
    }

    private TurretBlueprint selectedTurret;

    public void SelectStandardMachineGun()
    {
            Debug.Log("Selected Standard MachineGun");
            Manager.instance.SelectTurretToBuild(standardMachineGun);
            selectedTurret = standardMachineGun;
        
    }

    public void SelectMissileLauncher()
    {
            Debug.Log("Selected Missile Launcher");
            Manager.instance.SelectTurretToBuild(missileLauncher);
            selectedTurret = missileLauncher;
        
    }

    public void SelectLaserGun()
    {
            Debug.Log("Selected Laser gun");
            Manager.instance.SelectTurretToBuild(laserGun);
            selectedTurret = laserGun;
        
    }
}
