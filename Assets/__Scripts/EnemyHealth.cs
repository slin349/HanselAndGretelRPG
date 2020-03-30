using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public GameObject expCube;

    override public void TakeDamage(float amount)
    {
        currhealth-= amount;
        if (currhealth <= 0)
        {
            print("Died");
            isDead = true;
            animator.SetBool("isDead", true);
            //dropping 4 exp orbs
            DropExp();
        }
        else
        {
            print("Took damage");
            animator.SetTrigger("hit");
        }
    }

    private void DropExp()
    {
            for (int i = 0; i < 4; i++)
            {
                Instantiate(expCube, new Vector3(transform.position.x + i, transform.position.y + 1.5f, transform.position.z + i), transform.rotation);
            }
    }
}
