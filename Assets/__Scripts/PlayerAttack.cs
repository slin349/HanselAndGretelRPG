using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public int attackDamage = 1;
    public float attackRange = 2f;
    public float attackRate = 0.5f;
    public GameObject grenade;
    public int numOfGrenades = 1;
    private float nextAttack;
    private Animator animator;
    private int _level;
    private const int DAMAGE_MULTIPLIER = 2;

    private Health health;

    private void Start()
    {
        health = GetComponentInParent<Health>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Only respond to click events if the player is not dead
        if (!health.isDead)
        {
            if (Input.GetMouseButton(0) && Time.time > nextAttack)
            {
                nextAttack = Time.time + attackRate;
                DoAttack();
                animator.SetTrigger("attack");
            }
        }
    }

    private void DoAttack()
    {

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
