using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoHome
{
    public class Collectable : MonoBehaviour
    {
        public int value = 1;
        public void Collect()
        {
            //null = set to nothing
            GameManager.Instance.AddScore(value);
            //FindObjectOfType<GameManager>().AddScore(value);

            // Destroy self for now - or do animations
            Destroy(gameObject);
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



