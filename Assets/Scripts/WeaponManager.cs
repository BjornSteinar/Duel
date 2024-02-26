using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject weapon;
    public PlayerMovement pM;
    public void EnableWeaponCollider(int isEnable)
    {
        //Check if th character is holding a weapon
        if(weapon == isActiveAndEnabled)
        {
            var col = weapon.GetComponent<MeshCollider>();

            //Check if the weapon has a collider
            if(col != null) 
            { 
                if(isEnable == 1)
                {
                    col.enabled = true;
                }
                else
                {
                    col.enabled = false;
                }
            }
        }
    }
    public void EnableMovement(bool enable)
    {
        if(enable == false)
        {
            pM.canMove = true;
        }
        else
        {
            pM.canMove = false;
        }
    }
}
