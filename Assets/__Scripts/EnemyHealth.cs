using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public GameObject expCube;
    private PointSystem pointSystem;

    override public void Start()
    {
        animator = GetComponentInChildren<Animator>();
        currhealth = maxHealth;
        pointSystem = Object.FindObjectOfType<PointSystem>();
    }

    override public void TakeDamage(float amount)
    {
        currhealth-= amount;
        if (currhealth <= 0)
        {
            if (gameObject.tag == "Green")
            {
                pointSystem.points += 10;
            } else if (gameObject.tag == "Blue")
            {
                pointSystem.points += 5; 
            }
            pointSystem.pointsText.text = "Points: " + pointSystem.points.ToString();
            print("Died");
            isDead = true;
            animator.SetBool("isDead", true);
            //dropping 4 exp orbs
            DropExp();
            Destroy(gameObject);
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
