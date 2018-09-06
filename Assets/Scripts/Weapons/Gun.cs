using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject bullet;
    public Transform spawnPoint;
    public KeyCode fireButton;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If the fireButton set is pressed (down)
        if (Input.GetKeyDown(fireButton))
        {
            // Instantiate a new bullet from prefab "bullet"
            GameObject clone = Instantiate(bullet, transform.position, transform.rotation);
            // Get the component from the new bullet
            Bullet newBullet = clone.GetComponent<Bullet>();
            // Tell the bullet to Fire()
            newBullet.Fire(transform.forward);
        }
    }
}
