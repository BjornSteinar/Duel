using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttirbutes : MonoBehaviour
{
    public HealthController hc;
    public Animator playerAnim;
    public Animator enemyAnim;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<HealthController>().TakeDamage(hc.attack);
            enemyAnim.Play("Hit");
        }
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthController>().TakeDamage(hc.attack);
            playerAnim.Play("Hit");
        }
    }

}
