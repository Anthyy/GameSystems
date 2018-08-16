﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 10f;
    public Rigidbody rigid;
    public float rayDistance = 1f;

    private bool isGrounded = true;

    // Implement this OnDrawGizmosSelected if you want to draw gizmos only if the object is selected
    private void OnDrawGizmos()
    {
        Ray groundRay = new Ray(transform.position, Vector3.down); // telling the ray to go down
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundRay.origin, groundRay.origin + groundRay.direction * rayDistance);
    }

    bool IsGrounded()
    {
        Ray groundRay = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        // Casts a line beneath the player
        if (Physics.Raycast(groundRay, out hit, rayDistance)) // out = outside of this statement
        { // These {} are called "scope"
            // Return true if grounded
            return true; // any code right below this would not run if the if statement above ended with a ;
        }
        // Return false if NOT grounded
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal") * moveSpeed;
        float inputV = Input.GetAxis("Vertical") * moveSpeed;
        Vector3 moveDir = new Vector3(inputH, 0f, inputV); // inputH = x, 0f = y, inputV = Z (y is 0 because you don't want up & down movement)
        Vector3 force = new Vector3(moveDir.x, rigid.velocity.y, moveDir.z);

        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded()) // or (Input.Getbutton("Jump"))
        {
            force.y = jumpHeight; 
        }

        rigid.velocity = force;

        // If the user pressed a key (moveDir has values in it other than 0)
        if(moveDir.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
        }
    }
}