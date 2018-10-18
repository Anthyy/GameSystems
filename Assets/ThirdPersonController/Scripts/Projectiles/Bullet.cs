using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonController
{
    public class Bullet : MonoBehaviour
    {
        public int damage = 50;
        public float speed = 5f;
        public Rigidbody rigid;

        // Method for 'Firing' the bullet using Rigidbody
        public void Fire(Vector3 direction)
        {
            // Add a force in the given 'direction' variable and use impulse
            rigid.AddForce(direction * speed, ForceMode.Impulse);
        }

        // When the bullet hits an object, this function is called
        void OnTriggerEnter(Collider other)
        {
            // Get the Enemy component from it
            Enemy enemy = other.GetComponent<Enemy>();
            // If it is indeed an Enemy
            if (enemy)
            {
                // Deal damage to the Enemy
                // enemy.DealDamage(damage);
                // Destroy the bullet
                Destroy(gameObject);
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

