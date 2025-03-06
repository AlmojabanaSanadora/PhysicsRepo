using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FPSCamara : MonoBehaviour
{
    private Transform Camera;
    public Vector2 Sensibility;
    // Start is called before the first frame update
    void Start()
    {
        Camera = transform.Find("Camera");
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        if (hor != 0)
        {
            transform.Rotate(Vector3.up * hor * Sensibility.x);
        }

        if (ver != 0)
        {
            //Camera.Rotate(Vector3.left * ver * Sensibility.y);
            float angle = (Camera.localEulerAngles.x - ver * Sensibility.y + 360) % 360;
            if (angle > 180) { angle -= 360; }
            angle = Mathf.Clamp(angle, -80, 80);
            Camera.localEulerAngles = Vector3.right * angle;
        }
    }
}

