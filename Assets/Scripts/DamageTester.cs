using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTester : MonoBehaviour
{
    public HealthController playerHc;
    public HealthController enemyHc;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            playerHc.DealDamage(enemyHc.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            enemyHc.DealDamage(playerHc.gameObject);
        }
    }
}
