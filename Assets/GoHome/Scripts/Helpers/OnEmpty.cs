using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GoHome
{
    public class OnEmpty : MonoBehaviour
    {
        public UnityEvent onEmpty;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // Is there no children left
            if (transform.childCount == 0)
            {
                // Invoke on Unity Event
                onEmpty.Invoke();
                // Disable gameObject
                gameObject.SetActive(false);
            }
        }
    }
}
