using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GoHome
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 10f;
        public float maxVelocity = 20f;
        public Rigidbody rBody;
        public UnityEvent onDeath; // UnityEvent creates a box in the inspector where you can set an event/function
        public ParticleSystem explosionParticles;


        private Vector3 startPosition;


        private void Start()
        {
            // 'transform' refers to the Transform component of the GameObject the script is attached to
            // 'gameObject' refers to the GameObject that the script is attached to

            // Record starting position
            startPosition = transform.position;
            // Detach particles from children
            explosionParticles.transform.SetParent(transform.parent);
        }
        // Constrain the velocity per update
        private void Update()
        {
            /*Vector3 newVector = new Vector3(0, 10, 0);
            Vector3 newVector2 = new Vector3(1, 20, 0);
            Vector3 result = newVector + newVector2;*/
            
            Vector3 vel = rBody.velocity;
            if(Mathf.Abs(vel.x) > maxVelocity) // Mathf.Abs converts a negative number to an 'absolute' number (a positive number)
            {
                vel.x = vel.normalized.x * maxVelocity;
            }
            if(vel.z > maxVelocity)
            {
                vel.z = vel.normalized.x * maxVelocity;
            }
        }
        // Collect item on trigger enter
        private void OnTriggerEnter(Collider other)
        {
            // Try checking if player hits a killbox || if the enemy hits the player
            if (other.name.Contains("KillBox") || other.name.Contains("Enemy"))
            {
                // Then we're gonna die
                Death();
            }

            // Try getting collectable component from other collider
            Collectable collectable = other.GetComponent<Collectable>();
            // If it's not null
            if (collectable)
            {
                // Collect the item
                collectable.Collect();
            }
        }
        // Input method for movement
        public void Move(float inputH, float inputV)
        {
            // Generate direction from input (horizontal & vertical)
            Vector3 direction = new Vector3(inputH, 0, inputV);
            // Set velocity to direction with speed
            Vector3 vel = rBody.velocity;
            vel.x = direction.x * speed;
            vel.z = direction.z * speed;
            rBody.velocity = vel;
           
        }

        public void Death()
        {
            // Setting position of particles to player position
            explosionParticles.transform.position = transform.position; // Wherever the script is is where transform.position is
            explosionParticles.Play();

            transform.position = startPosition;
            rBody.velocity = Vector3.zero; // Cancels the velocity when you die
                                         
            onDeath.Invoke();
        }

    }

}
