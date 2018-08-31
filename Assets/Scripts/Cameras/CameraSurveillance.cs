using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSurveillance : MonoBehaviour
{
    public Camera[] cameras; // Array of cameras to switch between
    public KeyCode prevKey = KeyCode.Q; // Filter back to prev cam
    public KeyCode nextKey = KeyCode.E; // Filter forward to next cam
    private int camIndex; // Current cam index from array
    private int camMax; // Max amount of cams in array
    private Camera current; // Current camera selected

    // Use this for initialization
    void Start()
    {
        cameras = GetComponentsInChildren<Camera>(true);
        // Last index of array = Array.Length -1
        camMax = cameras.Length - 1;
        // Activate the default camera;
        ActivateCamera(camIndex);
    }
    void ActivateCamera(int camIndex)
    {
        // Loop through all surveillance cameras
        for (int i = 0; i < cameras.Length; i++)
        {
            Camera cam = cameras[i];
            // If the current index matches the argument camIndex
            if (i == camIndex)
            {
                //Enable this camera
                cam.gameObject.SetActive(true);
            }
            else // ...otherwise
            {
                // disable camera
                cam.gameObject.SetActive(false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        // If the next key is pressed
        if (Input.GetKeyDown(nextKey)) // we've already established above that 'Q' and 'E' are the keys
        {
            // increase index
            camIndex++;
            if (camIndex > camMax)
            {
                camIndex = 0;
            }
            ActivateCamera(camIndex);
        }
        if (Input.GetKeyDown(prevKey)) 
        {
            // decrease index
            camIndex--;            
            if (camIndex < 0)
            {
                camIndex = camMax;
            }
            ActivateCamera(camIndex);
        }
    }
}
        
