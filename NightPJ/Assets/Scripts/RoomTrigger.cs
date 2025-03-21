using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public int roomNumber; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameHandler.instance != null)
            {
                GameHandler.instance.SetActiveRoom(roomNumber);
            }
        }
    }
}