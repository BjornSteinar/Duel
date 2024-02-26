using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int health;
    public int attack;
    public void TakeDamage(int amount)
    {
        health -= amount;
    }
    public void DealDamage(GameObject target)
    {
        var hc = target.GetComponent<HealthController>();
        if(hc != null)
        {
            hc.TakeDamage(attack);
        }
    }
}
