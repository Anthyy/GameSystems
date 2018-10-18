using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class Player : MonoBehaviour
    {
        public float upForce = 5f;
        public bool isDead = false;
        public Rigidbody2D rBody;
       
        // Update is called once per frame
        void Update()
        {
            // If mouse button is down
            if (Input.GetMouseButtonDown(0))
            {
                // Make bird flap
                Flap();
            }
        }

        // Makes the bird flap when called
        void Flap()
        {
            // Is the bird not dead?
            if (!isDead) // or if (isDead == false)
            {
                // Cancel velocity
                rBody.velocity = Vector2.zero;
                // Give the bird an upward force using impulse
                // new Vector2(0, upForce) == Vector2.up * upForce
                rBody.AddForce(new Vector2(0, upForce), ForceMode2D.Impulse);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            GameManager.Instance.AddScore(1);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            GameManager.Instance.GameOver();
        }
    } 
}

