using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public GameObject explosionParticles;
    public float blastRadius = 5f;
    public float blastForce = 700f;

    float countdown;
    bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        //setting delay for explosion
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        //reducing the value of countdown per frame
        countdown -= Time.deltaTime;

        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        //Show effect at current position and rotation
        Instantiate(explosionParticles, transform.position, transform.rotation);

        //Get nearby objects and puts them into an array
            Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
            foreach (Collider nearbyObject in colliders)
            {
                Health dealDamage = nearbyObject.GetComponent<Health>();

                //check if nearby objects are not rigidbody
                if (dealDamage != null)
                {
                //add damage when grenade explodes
                dealDamage.TakeDamage(5);
                }
            }

        //Delete grenade
        Destroy(gameObject);
    }
}
