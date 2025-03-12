using UnityEngine;

public class InvisibleObject : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().enabled = false; // Hides the object
    }
}