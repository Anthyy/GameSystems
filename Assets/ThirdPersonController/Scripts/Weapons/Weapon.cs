using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThirdPersonController
{
    public abstract class Weapon : MonoBehaviour
    {

        public GameObject projectile;
        public Transform spawnPoint; // Where the "bullets" (spheres) will spawn from

        public int damage = 100;
        public int ammo = 30;
        public int currentAmmo = 0;

        public float accuracy = 1f;
        public float range = 10f;
        public float rateOfFire = 5f;

        public abstract void Attack();

        public void Reload()
        {
            // Reset currentAmmo
            currentAmmo = ammo;
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

