using UnityEngine;

public class Turret : MonoBehaviour
{
    private TurretsDataManager turretsDataManager;
    public enum TurretType { MachineGun, MissileLauncher, LaserGun }
    public TurretType turretType;

    public int turretLevel = 1;

    public Transform target;
    private Enemy targetEnemy;

    [Header("General")]
    public float range = 15f;
    private GameObject rangeIndicator;

    [Header("Use Bullets(default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")]
    public int damageOverTime = 30;
    public float slowPct = .5f;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Setup Fields")]
    private string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed = 5f;

    private int firePointIndex = 1;

    public Transform firePoint;
    public Transform firePoint1;
    public Transform firePoint2;
    public int sellPrice = 50;

    void Start()
    {
        turretsDataManager = (TurretsDataManager)FindFirstObjectByType(typeof(TurretsDataManager));
        switch (turretType)
        {
            case TurretType.MachineGun:
                if (turretLevel == 1)
                    fireRate += turretsDataManager.LoadData().lastMachineGunLvl1FireRateBuyed;
                else if (turretLevel == 2)
                    fireRate += turretsDataManager.LoadData().lastMachineGunLvl2FireRateBuyed;
                break;
            case TurretType.MissileLauncher:
                if (turretLevel == 1)
                    fireRate += turretsDataManager.LoadData().lastMissileLauncherLvl1FireRateBuyed;
                else if (turretLevel == 2)
                    fireRate += turretsDataManager.LoadData().lastMissileLauncherLvl2FireRateBuyed;
                break;
            case TurretType.LaserGun:
                if (turretLevel == 1)
                    fireRate += turretsDataManager.LoadData().lastLaserGunLvl1FireRateBuyed;
                else if (turretLevel == 2)
                    fireRate += turretsDataManager.LoadData().lastLaserGunLvl2FireRateBuyed;
                break;
        }
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null)
        {
            if (turretType == TurretType.LaserGun)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }

        LockOnTarget();
        if (turretType == TurretType.LaserGun)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    private void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPct);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized * 0.5f;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

    }

    private void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Shoot()
    {
        if (turretType == TurretType.MissileLauncher && turretLevel == 2)
        {
            Transform selectedFirePoint;

            switch (firePointIndex)
            {
                case 0:
                    selectedFirePoint = firePoint;
                    firePointIndex++;
                    break;
                case 1:
                    selectedFirePoint = firePoint1;
                    firePointIndex++;
                    break;
                case 2:
                    selectedFirePoint = firePoint2;
                    firePointIndex = 0;
                    break;
                default:
                    selectedFirePoint = firePoint;
                    break;
            }

            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, selectedFirePoint.position, selectedFirePoint.rotation);

            Bullet bullet = bulletGO.GetComponent<Bullet>();

            if (bullet != null)
                bullet.Seek(target);
        }else
        {
            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            Bullet bullet = bulletGO.GetComponent<Bullet>();

            if (bullet != null)
                bullet.Seek(target);
        }
    }

    public void ShowRange()
    {
        if (rangeIndicator != null)
            return; 

        rangeIndicator = new GameObject("RangeIndicator");
        rangeIndicator.transform.SetParent(transform);

        LineRenderer lineRenderer = rangeIndicator.AddComponent<LineRenderer>();

        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.positionCount = 100;

        Vector3[] positions = new Vector3[100];
        float radius = range;
        for (int i = 0; i < 100; i++)
        {
            float angle = i * Mathf.PI * 2 / 100;
            positions[i] = new Vector3(Mathf.Sin(angle) * radius, 0, Mathf.Cos(angle) * radius) + transform.position;
        }

        lineRenderer.SetPositions(positions);

        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
    }

    public void HideRange()
    {
        if (rangeIndicator != null)
        {
            Destroy(rangeIndicator); // Destroy the range indicator if it exists
            rangeIndicator = null; // Set rangeIndicator to null after destruction
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
