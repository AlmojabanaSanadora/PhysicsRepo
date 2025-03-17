using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{

    public GameObject bullet;
    public Transform spawnPoint;

    public float shotForce = 0.1f;
    public float shotRate = 0.5f;

    private float shotRateTime = 0;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {

            if (Time.time > shotRateTime)
            {
                Rigidbody rb = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation).GetComponent<Rigidbody>();
                rb.linearVelocity = spawnPoint.forward * 32f;
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

                shotRateTime = Time.time + shotRate;
                Destroy(rb.gameObject, 2f);

            }
        }
    }
}
