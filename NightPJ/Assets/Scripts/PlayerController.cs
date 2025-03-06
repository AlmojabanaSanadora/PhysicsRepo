using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private new Rigidbody rigidbody;

    public float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");


        if (hor != 0 || ver != 0)
        {
            Vector3 direction = (transform.forward * ver + transform.right * hor).normalized;

            rigidbody.velocity = direction * movementSpeed;
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }
    }
}