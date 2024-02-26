using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttirbutes : MonoBehaviour
{
    public HealthController hc;
    public Animator playerAnim;
    public Animator enemyAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<HealthController>().TakeDamage(hc.attack);

        }
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthController>().TakeDamage(hc.attack);
            playerAnim.SetTrigger("hit");
        }
    }

}
