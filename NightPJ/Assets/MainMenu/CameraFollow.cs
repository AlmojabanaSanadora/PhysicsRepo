using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Arrastra el jugador aquí en el Inspector
    public Vector3 offset; // Ajusta el offset en el Inspector

    void Update()
    {
        if (player != null) // Solo sigue al jugador si existe
        {
            transform.position = player.position + offset;
        }
    }
}
