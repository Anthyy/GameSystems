using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonController
{
    public class Explosive : Projectile
    {
        public float explosionRadius = 5f;
        public GameObject explosion;

        public override void OnCollisionEnter(Collision col)
        {
            string tag = col.collider.tag;
            if (tag != "Player" && tag != "Weapon")
            {
                print(col.collider.tag);
                print(col.collider.name);
                Effects(); // Spawns a particle
                Explode(); // Deals damage to surrounding enemies
            }

        }

        void Effects()
        {
            // Spawn a new explosion GameObject
            Instantiate(explosion, transform.position, transform.rotation);
        }

        void Explode()
        {
            // Detect collision with objects using radius
            Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
            // Loop through everything we hit
            foreach (var hit in hits)
            {
                // Try getting the enemy component
                Enemy e = hit.GetComponent<Enemy>();
                // If we hit an enemy
                if (e)
                {
                    // Note: You can calculate falloff damage here

                    // Deal damage to the enemy
                    e.DealDamage(damage);
                }
            }
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

