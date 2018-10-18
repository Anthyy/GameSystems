using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonController
{
    public class Incendiary : Projectile
    {
        public float dotDuration = 2f;

        public override void OnCollisionEnter(Collision col)
        {
            Enemy e = col.collider.GetComponent<Enemy>();
            if (e)
            {
                // Disable bullet effects
                // Burn the enemy
                Burn(e);
            }
        }

        IEnumerator Burn(Enemy enemy)
        {
            yield return new WaitForSeconds(dotDuration);
            enemy.DealDamage(damage);
            // Start it again
            StartCoroutine(Burn(enemy));
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

