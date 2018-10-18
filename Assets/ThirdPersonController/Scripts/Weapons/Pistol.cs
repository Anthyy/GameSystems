using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonController
{
    public class Pistol : Weapon
    {
        public override void Attack()
        {
            /* If the fireButton set is pressed (down)
            if (Input.GetKeyDown(fireButton)) */
            {
                // Instantiate a new bullet from prefab "bullet"
                GameObject clone = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
                // Get the component from the new bullet
                Projectile newBullet = clone.GetComponent<Projectile>();
                // Tell the bullet to Fire()
                newBullet.Fire(transform.forward);
            }
        }
    }
}


