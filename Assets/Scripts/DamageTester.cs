using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTester : MonoBehaviour
{
    public HealthController playerHc;
    public HealthController enemyHc;
    public bool blocking;
    public Animator enemyAnim;
    public CapsuleCollider enemyCollider;
    public int weaponType = 3;
    public bool drawn;
    public GameObject weapon;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            drawWeapon();
        }
        if (Input.GetKey(KeyCode.T))
        {
            blocking = true;
            enemyAnim.SetBool("Blocking", true);
            enemyCollider.enabled = false;
        }
        else if (Input.GetKeyUp(KeyCode.T))
        {
            blocking = false;
            enemyAnim.SetBool("Blocking", false);
            enemyCollider.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            swingWeapon();
            //enemyAnim.ResetTrigger("swing");
        }
    }
    void swingWeapon()
    {
        // Heavy weapon
        if (weaponType == 3 && drawn && !blocking)
        {
            // enemyAnim.Play("Heavy weapon attack");
            enemyAnim.SetTrigger("swing");
        }
    }
    void drawWeapon()
    {
        // Heavy weapon
        if (weaponType == 3)
        {
            if (drawn)
            {
                enemyAnim.SetInteger("equip", 0);
                weapon.SetActive(false);
                drawn = false;
            }
            else if (!drawn)
            {
                enemyAnim.SetInteger("equip", 3);
                enemyAnim.SetTrigger("drawing");
                weapon.SetActive(true);
                drawn = true;
            }
        }
    }
}

