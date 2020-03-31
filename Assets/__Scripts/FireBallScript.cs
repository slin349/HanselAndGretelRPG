using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour
{
    public GameObject explosionVFX;
    public float blastRadius;

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
            Health dealDamage = nearbyObject.GetComponent<Health>();

            //check if nearby objects are not rigidbody
            if (dealDamage.isResistve)
            {
                dealDamage.TakeDamage(0);
            }
            else if (dealDamage != null)
            {
                //add damage when grenade explodes
                dealDamage.TakeDamage(5);
            }
        }
    }
}
