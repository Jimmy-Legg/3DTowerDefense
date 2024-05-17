using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayersHealthBarText : MonoBehaviour
{
    public Text Health;

    void Update()
    {
        Health.text = "Health: " + PlayerStats.health.ToString();
    }
}
