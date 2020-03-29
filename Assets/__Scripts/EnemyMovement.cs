using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float playerDistance;
    public float awareAI = 10f;
    public float AIMoveSpeed;
    public float dmaping = 6.0f;

    public Transform[] navPoint;
    public UnityEngine.AI.NavMeshAgent agent;
    public int destPoint = 0;
    public Transform goal;

    private Animator animator;
    private Health health;

    private Health playerHealth;

    private void Start()
    {
        health = GetComponentInChildren<Health>();
        animator = GetComponentInChildren<Animator>();
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = goal.position;
        agent.autoBraking = false;
        // Default to walking animation
        animator.SetInteger("condition", 1);

        playerHealth = player.GetComponentInChildren<Health>();

    }

    private void Update()
    {
        // Check if character is still alive
        if (!health.isDead)
        {
            Move();
        }
        else
        {
            // Prevent nav agent from moving after death
            agent.ResetPath();
        }
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);


    }

    private bool isDistanceCheck = false;
    private float timeLeft = 3.0f;

    private float attackRate = 2.0f;
    private float nextAttack;

    void Move()
    {
        playerDistance = Vector3.Distance(player.position, transform.position);

        // Attack Logic
        // Should move this into its own function later
        if (playerDistance < 3.0f && !playerHealth.isDead)
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

        // Movement logic
        if (playerDistance < awareAI && !playerHealth.isDead)
        {
            LookAtPlayer();
        }
        if (playerDistance < awareAI && !playerHealth.isDead)
        {
            if (playerDistance > 2f)
            {
                // Run
                animator.SetInteger("condition", 2);
                Chase();
            }
            else
            {
                // Walk
                animator.SetInteger("condition", 1);
                GoToNextPoint();
            }
        }
        {
            if (agent.remainingDistance < 0.5f)
            {
                animator.SetInteger("condition", 1);
                GoToNextPoint();
            }
        }
    }

    void LookAtPlayer()
    {
        transform.LookAt(player);
    }

    void GoToNextPoint()
    {
        if (navPoint.Length == 0) return;
        agent.destination = navPoint[destPoint].position;
        destPoint = (destPoint + 1) % navPoint.Length;
    }

    void Chase()
    {
        agent.destination = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }
}
