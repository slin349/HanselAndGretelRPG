using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public GameObject expCube;

    override public void TakeDamage(int amount)
    {
        currhealth-= amount;
        if (currhealth <= 0)
        {
            print("Died");
            isDead = true;
            animator.SetBool("isDead", true);
            //dropping 4 exp orbs
            for (int i = 0; i < 4; i++)
            {
                Instantiate(expCube, new Vector3(transform.position.x + i, transform.position.y, transform.position.z + 1), transform.rotation);
            }
        }
        else
        {
            print("Took damage");
            animator.SetTrigger("hit");
        }
    }
}
