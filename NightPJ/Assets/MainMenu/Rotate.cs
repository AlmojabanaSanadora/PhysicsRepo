using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speedRotate = 0.2f;

    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0) * speedRotate);
    }
}
