using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 100;
    public float speed = 5f;
    public float range = 50f;
    public GameObject impact;
    public Rigidbody rBody;
    public virtual void Fire(Vector3 direction)
    {
        // Shoot the bullet off in given direction x speed (direction x speed = velocity)
        rBody.AddForce(direction * speed, ForceMode.Impulse);
    }

    public virtual void OnCollisionEnter(Collision collision)
    {

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
