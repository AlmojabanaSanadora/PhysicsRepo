using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameHandler : MonoBehaviour
{
    public GameObject[] doors1;
    public TextMeshProUGUI enemyCountText;
    private int calcEnemyCount;
    public static GameHandler instance;

    private bool doorsOpen = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (EnemySpawner.instance == null)
        {
            return;
        }

        calcEnemyCount = EnemySpawner.instance.totalEnemiesToSpawn;
        enemyCountText.text = calcEnemyCount.ToString();
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
            return;
        }

        foreach (GameObject door in doors1)
        {
            Vector3 targetPosition = new Vector3(door.transform.position.x - 1.1f, door.transform.position.y, door.transform.position.z);
            StartCoroutine(SlideDoor(door, targetPosition, 2f));
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
            door.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            yield return null;
        }

        door.transform.position = targetPosition;
    }
}