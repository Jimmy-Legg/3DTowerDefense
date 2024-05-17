using System;
using UnityEngine;
using UnityEngine.UI;
using static DataManager;

public class Enemy : MonoBehaviour
{

    public HealthBar healthBar;

    public Text healthText;

    public GameObject deathEffect;

    [HideInInspector]
    public float speed;

    public float startSpeed = 5f;

    [HideInInspector]
    public float health;

    public float startHealth = 100f;

    public static int worth;
    public int startWorth = 50;


    private void Start()
    {
        speed = startSpeed;
        health = startHealth + (WaveSpawner.waveIndex * 40 * DifficultyModifier.Multiplicator());
        worth = startWorth;
        healthBar.SetMaxHealth(health);
        UpdateHealthText();
    }


    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.SetHealth(health);
        UpdateHealthText();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        PlayerStats.money += worth;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(gameObject);
    }

    private void UpdateHealthText()
    {
        healthText.text = "HP: " + string.Format("{0:00.0}", health);
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }
}
