using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class GameHandler : MonoBehaviour
{
    public GameObject[] doors1;
    public TextMeshProUGUI enemyCountText;
    public int calcEnemyCount = 5;
    public static GameHandler instance;

    private bool doorsOpen = false;

        private void Awake()
    {
        enemyCountText.text = calcEnemyCount.ToString();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void DecrementEnemyCount()
    {
        if (calcEnemyCount > 0) 
        {
            calcEnemyCount--;
            UpdateEnemyCount();
            RunDoors();
        }
    }

    public void UpdateEnemyCount()
    {
        enemyCountText.text = calcEnemyCount.ToString();
    }

        public void OpenFirstDoors()
    {
        if (doors1 == null || doors1.Length == 0) 
        {
            Debug.LogError("No doors found!");
            return;
        }

        foreach (GameObject door in doors1)
        {
        Vector3 targetPosition = new Vector3(door.transform.position.x - 1.1f, door.transform.position.y, door.transform.position.z);
        StartCoroutine(SlideDoor(door, targetPosition, 2f)); // Slide over 2 seconds
        }
    }

    private void RunDoors()
    {
        if (calcEnemyCount == 0 && !doorsOpen)
        {
            OpenFirstDoors();
            doorsOpen = true;
        }
    }

    private IEnumerator SlideDoor(GameObject door, Vector3 targetPosition, float duration)
{
    Vector3 initialPosition = door.transform.position;
    float elapsedTime = 0f;

    while (elapsedTime < duration)
    {
        elapsedTime += Time.deltaTime;
        door.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration); // Smoothly interpolate position
        yield return null;
    }

    door.transform.position = targetPosition; // Ensure the final position is set
}
}
