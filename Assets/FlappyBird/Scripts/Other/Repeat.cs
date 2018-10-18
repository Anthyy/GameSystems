using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class Repeat : MonoBehaviour
    {
        public float moveSpeed = 10f;
        public bool isRepeating = true;
        public BoxCollider2D box; // or SpriteRenderer rend;
        private float width;

        // Use this for initialization
        void Start()
        {
            if (box) // or if (rend) if using the alternate variable above
            {
                // Get width from collider and scale by transform
                width = box.size.x; // or width = rend.bounds.size.x if using the alternate variable above
            }            
        }

        // Update is called once per frame
        void Update()
        {
            // Get position 
            Vector3 pos = transform.position;
            // Move position
            pos += Vector3.left * moveSpeed * Time.deltaTime;
            // If leaving left side of screen
            if (pos.x < -width && isRepeating)
            {
                // Repeat = Move to opposite side of screen (object pooling [which is reusing objects instead of destroying them])
                float offset = (width - .01f) * 2;
                Vector3 newPosition = new Vector3(offset, 0, 0);
                pos += newPosition;
            }
            transform.position = pos;
        }
    }
}

