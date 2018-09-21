using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    // public bool isOpen = false;
    public Animator anim;

    public override void Interact()
    {
        // Toggle and animate door
        bool isOpen = anim.GetBool("IsOpen");
        anim.SetBool("IsOpen", !isOpen);
        /*isOpen = !isOpen; // Toggle isOpen                | For use with the 
        anim.SetBool("IsOpen", isOpen); // Animate door     | bool up the top */
    }
}
