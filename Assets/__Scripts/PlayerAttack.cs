using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public int attackDamage = 1;
    public float attackRange = 1f;
    public float attackRate = 1f;
    public GameObject grenade;
    public int numOfGrenades = 1;

    private float nextAttack;
    private Animator animator;


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time > nextAttack)
        {
            nextAttack = Time.time + attackRate;
            DoAttack();
            animator.SetTrigger("attack");
        }

        //If Q is pressed, initiate grenade
        if (Input.GetKeyDown(KeyCode.Q) && numOfGrenades != 0)
        {
            ThrowGrenade();
        }

    }

    private void DoAttack()
    {

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Health>().TakeDamage(attackDamage);
        }

    }

    private void ThrowGrenade()
    {
        numOfGrenades--;
        //Instantiate grenade then add force to "throw" it
        GameObject go = Instantiate(grenade, transform.position, transform.rotation);
        go.GetComponent<Rigidbody>().AddForce(transform.forward * 300);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
