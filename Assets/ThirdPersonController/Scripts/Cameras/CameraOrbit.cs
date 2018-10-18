using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonController
{
    public class CameraOrbit : MonoBehaviour
    {
        public Transform target; // Target object to orbit around
        public bool hideCursor = true; // Is the cursor hidden?
        [Header("Orbit")]
        public Vector3 offset = new Vector3(0, 0, 0); // Vector offset from original position
        public float xSpeed = 120f; // x orbit speed
        public float ySpeed = 120f; // y orbit speed
        public float yMinLimit = -20f; // y clamp min (maximum Y limit)
        public float yMaxLimit = 80f; // y clamp max (maximum Y limit)
        public float distanceMin = 0.5f; // Min distance to target
        public float distanceMax = 15f; // Max distance to target

        [Header("Collision")]
        public bool cameraCollision = true; // Is cam collision enabled?
        public float cameraRadius = 0.3f; // Radius of cam collision cast
        public LayerMask ignoreLayers; // Layers ignored by collision

        private Vector3 originalOffset; // Original offset at start of game
        private float distance; // Current distance to camera
        private float rayDistance = 1000f; // Max distance ray can check of collisions

        private float x = 0f; // x degrees of rotation
        private float y = 0f; // y degrees of rotation


        // Use this for initialization
        void Start()
        {
            // Detach camera from parent
            transform.SetParent(null);

            // Is the cursor supposed to be hidden?
            if (hideCursor) // if hide cursor is true
            {
                // Lock...
                Cursor.lockState = CursorLockMode.Locked;
                //...hide the Cursor
                Cursor.visible = false;
            }

            // Calculate original offset from target position
            originalOffset = transform.position - target.position;
            // Set ray distance to current distance magnitude of camera
            rayDistance = originalOffset.magnitude;

            // Get camera rotation 
            Vector3 angles = transform.eulerAngles;
            //set x and y degrees to current camera rotation
            x = angles.y;
            y = angles.x;
        }

        // Update is called once per frame

        void Update()
        {
            // If target has been set
            if (target)
            {
                // rotate the camera based on Mouse X and Mouse Y inputs
                x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime; // "Input.GetAxis("Mouse X/ Mouse Y") is referencing axes in the InputManager
                y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

                // Clamp the angle using a custom 'ClampAngle' function defined in this script
                y = ClampAngle(y, yMinLimit, yMaxLimit);

                // Rotate the transform using euler angles (y for X rotation and x for Y rotation)
                transform.rotation = Quaternion.Euler(y, x, 0);
            }
        }

        private void FixedUpdate()
        {
            // If a target has been set
            if (target)
            {
                // Is camera collision enabled?
                if (cameraCollision)
                {
                    // Create a ray starting from target's position and pointing backwards from camera 
                    Ray camRay = new Ray(target.position, -transform.forward);
                    RaycastHit hit;
                    // Shoot a sphere in a defined ray direction
                    if (Physics.SphereCast(camRay, cameraRadius, out hit, rayDistance, ~ignoreLayers, QueryTriggerInteraction.Ignore))
                    {
                        // Set current camera distance to hit object's distance
                        distance = hit.distance;
                        // Exit function (as it's void)
                        return;
                    }
                }
                // Set distance to original distance
                distance = originalOffset.magnitude;
            }
        }

        private void LateUpdate()
        {
            // If a target has been set
            if (target)
            {
                // calculate our localOffset from offset
                Vector3 localOffset = transform.TransformDirection(offset);
                // reposition camera to new position, taking into account Distance and localOffset
                transform.position = (target.position + localOffset) + -transform.forward * distance;
            }
        }

        // Clamps the angle between -360 and +360 degrees and using the min and max angle
        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360f)
            {
                angle += 360;
            }
            if (angle > 360)
            {
                angle -= 360;
            }
            return Mathf.Clamp(angle, min, max);
        }
    }
}

