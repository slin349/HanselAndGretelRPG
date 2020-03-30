﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Bomb, Sword
}

public class EnemyAttack : MonoBehaviour
{

    public AttackType attackType = AttackType.Sword;    // Default to sword

    public GameObject player;
    public GameObject grenade;


    private EnemyMovement enemyMovement;
    private Health health;
    private Health playerHealth;

    private bool isDistanceCheck = false;
    private float timeLeft = 3.0f;
    private float attackRate = 2.0f;
    private float nextAttack;

    private Animator animator;

    void Start()
    {
        enemyMovement = GetComponentInChildren<EnemyMovement>();
        health = GetComponentInChildren<Health>();  // Health of the enemy
        animator = GetComponentInChildren<Animator>();    

        playerHealth = player.GetComponentInChildren<Health>(); // Health of the player

    }

    void Update()
    {
        // If enemy is alive, check whether to attack or not
        if (!health.isDead)
        {
            if (attackType == AttackType.Bomb)
            {
                BombAttack();
            }
            else
            {
                SwordAttack();
            }
        }
    }

    void BombAttack()
    {
        if (enemyMovement.playerDistance < 5.0f && !playerHealth.isDead)
        {
            if (!isDistanceCheck)
            {
                print("You need to leave or else i wil lthrow bomb");
                isDistanceCheck = true;
            }
            else
            {
                timeLeft -= Time.deltaTime;
            }

            if (timeLeft <= 0.0f && Time.time > nextAttack)
            {
                // Throw the grenade here
                print("Attacking!");
                nextAttack = Time.time + attackRate;
                animator.SetBool("attack", true);
                GameObject go = Instantiate(grenade, transform.position, transform.rotation);
                go.GetComponent<Rigidbody>().AddForce(transform.forward * 300);
            }
      
        }
        else
        {
            animator.SetBool("attack", false);
            isDistanceCheck = false;
            timeLeft = 3.0f;
        }
    }

    void SwordAttack()
    {
        if (enemyMovement.playerDistance < 3.0f && !playerHealth.isDead)
        {
            if (!isDistanceCheck)
            {
                print("You need to leave or else I will attack.");
                isDistanceCheck = true;
            }
            else
            {
                timeLeft -= Time.deltaTime;
            }

            if (timeLeft <= 0.0f && Time.time > nextAttack)
            {
                nextAttack = Time.time + attackRate;
                print("Attacking !");
                animator.SetBool("attack", true);
                // Damage the player
                playerHealth.TakeDamage(1);
            }
        }
        else
        {
            animator.SetBool("attack", false);
            isDistanceCheck = false;
            timeLeft = 3.0f;
        }
    }

}