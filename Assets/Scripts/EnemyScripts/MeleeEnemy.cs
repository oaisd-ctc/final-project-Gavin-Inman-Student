using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : EnemyController
{

    void Start()
    {
       
    }

    void Update()
    {
        
    }


    //Melee Attack Agaist player
    public IEnumerator MeleeAttack()
    {
        if (distance <= attackDistance && attacking == false || isDashing == true)
        {
            attacking = true;
            canLook = false;
            warning.SetActive(true);

            yield return new WaitForSeconds(warningTime);
            warning.SetActive(false);
            attack.SetActive(true);

            yield return new WaitForSeconds(0.2f);
            attack.SetActive(false);
            canLook = true;

            yield return new WaitForSeconds(1);
            attacking = false;
        }    
        
    }
}
