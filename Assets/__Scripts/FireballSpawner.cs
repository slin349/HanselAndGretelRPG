using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    public GameObject projectile;
    public float projectileSpeed;
    public float timeBeforeDeletion;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameObject fireball = Instantiate(projectile, transform) as GameObject;
            Rigidbody rb = fireball.GetComponent<Rigidbody>();

            rb.velocity = transform.forward * projectileSpeed;

            Destroy(fireball, timeBeforeDeletion);
        }
    }
}
