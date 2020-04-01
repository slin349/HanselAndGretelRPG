﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour
{
    public GameObject explosionVFX;
    public float blastRadius;
    public float projectileSpeed = 2.0f;

    private EnemyAttack enemyAttack;

    private void Start()
    {
        enemyAttack = GetComponentInParent<EnemyAttack>();
        print(enemyAttack.currentPlayerPosition);
    }


    private void Update()
    {
        float step = projectileSpeed * Time.deltaTime;
        Vector3 endPos = enemyAttack.currentPlayerPosition;
        endPos.y -= 1.0f;


        transform.position = Vector3.MoveTowards(transform.position, endPos, step);
    }

    void OnTriggerEnter(Collider other)
    {
        //Destory fireball if collided
        Destroy(gameObject);
        //Deploy explosion effects
        Instantiate(explosionVFX, transform.position, transform.rotation);

        //Deal Damage
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Health health = nearbyObject.GetComponent<Health>();

            //check if nearby objects are not rigidbody
            if (health)
            {
                if (!health.isResistve)
                {
                    health.TakeDamage(5);
                }
            } 
            
        }
    }
}