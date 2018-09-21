using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerController player;
    private int weaponIndex = 0;

    private void Start()
    {
        // Select the first weapon
        player.SelectWeapon(weaponIndex);
    }

    // Update is called once per frame
    void Update()
    {
        WeaponSwitch();

        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        player.Move(inputH, inputV);

        if (Input.GetButtonDown("Jump"))
        {
            player.Jump();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            player.Attack();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            player.Interact();
        }
    }

    private void WeaponSwitch()
    {
        // Record current weapon index
        int currentIndex = weaponIndex;
        // If Q is pressed && weaponIndex > 0
        if (Input.GetKeyDown(KeyCode.Q) && weaponIndex > 0)
        {
            // decrement weaponIndex
            currentIndex--;
        }       
        
        // If E is pressed && weaponIndex <= length
        if(Input.GetKeyDown(KeyCode.E) && weaponIndex <= player.weapons.Length)
        {
            // increment weaponIndex
            currentIndex++;
        }

        // If currentIndex has changed
        if(currentIndex != weaponIndex)
        {
            // Update weapon index
            weaponIndex = currentIndex;
            // Select weaponIndex
            player.SelectWeapon(weaponIndex);
        }       
    }
}
