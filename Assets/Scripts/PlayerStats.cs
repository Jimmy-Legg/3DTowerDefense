using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public HealthBar healthBar;

    public static int money;
    public int startMoney = 300;

    public static float health;
    public float startHealth = 100f;

    public static int round;

    private void Start()
    {
        money = startMoney;
        health = startHealth;
        round = 0;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(health);
        }
        else
        {
            Debug.LogError("HealthBar is not assigned!");
        }
    }

    private void Update()
    {
        if (healthBar != null)
        {
            healthBar.SetHealth(health);
        }
        else
        {
            Debug.LogError("HealthBar is not assigned!");
        }
    }
}
