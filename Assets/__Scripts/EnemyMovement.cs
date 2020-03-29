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

    void Move()
    {
        playerDistance = Vector3.Distance(player.position, transform.position);
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
