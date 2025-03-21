using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameHandler : MonoBehaviour
{
    public GameObject[] doors1;
    public GameObject[] doors2;
    public GameObject[] doors3;
    public GameObject[] doors4;

    public TextMeshProUGUI enemyCountText;
    private int calcEnemyCount;
    public static GameHandler instance;

    private bool[] doorsOpen = new bool[4];
    private int currentRoom = 1;


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

    public void SetActiveRoom(int roomNumber)
    {
        if (roomNumber == currentRoom) return;


        CloseDoorsForRoom(currentRoom);

        currentRoom = roomNumber;

        OpenDoorsForRoom(currentRoom);

        EnemySpawner.instance.SetActiveRoom(currentRoom);
        calcEnemyCount = EnemySpawner.instance.totalEnemiesToSpawn;
        UpdateEnemyCount();
    }

public void DecrementEnemyCount()
    {
        if (calcEnemyCount > 0)
        {
            calcEnemyCount--;
            UpdateEnemyCount();

            if (calcEnemyCount == 0)
            {
                OpenDoorsForRoom(currentRoom);
            }
        }
            
    }

    public void UpdateEnemyCount()
    {
        enemyCountText.text = calcEnemyCount.ToString();
    }

    private void OpenDoorsForRoom(int roomNumber)
{
    GameObject[] doorsToOpen = GetDoorsForRoom(roomNumber);

    if (doorsToOpen == null || doorsToOpen.Length == 0)
    {
        return;
    }

    foreach (GameObject door in doorsToOpen)
    {
        DoorController doorController = door.GetComponent<DoorController>();
        if (doorController != null)
        {
            StartCoroutine(SlideDoor(door, doorController.openDirection, doorController.openDistance, 2f));
        }
        else
        {
            Debug.LogWarning($"Door {door.name} does not have a DoorController component!");
        }
    }
}

    private void CloseDoorsForRoom(int roomNumber)
{
    GameObject[] doorsToClose = GetDoorsForRoom(roomNumber);

    if (doorsToClose == null || doorsToClose.Length == 0)
    {
        return;
    }

    foreach (GameObject door in doorsToClose)
    {
        DoorController doorController = door.GetComponent<DoorController>();
        if (doorController != null)
        {
            StartCoroutine(SlideDoor(door, -doorController.openDirection, doorController.openDistance, 0.1f));
        }
        else
        {
            Debug.LogWarning($"Door {door.name} does not have a DoorController component!");
        }
    }
}

    private GameObject[] GetDoorsForRoom(int roomNumber)
    {
        switch (roomNumber)
        {
            case 1: return doors1;
            case 2: return doors2;
            case 3: return doors3;
            case 4: return doors4;
            default: return null;
        }
    }

    private IEnumerator SlideDoor(GameObject door, Vector3 direction, float distance, float duration)
{
    Vector3 initialPosition = door.transform.position;
    Vector3 targetPosition = initialPosition + door.transform.TransformDirection(direction.normalized) * distance;
    float elapsedTime = 0f;

    while (elapsedTime < duration)
    {
        elapsedTime += Time.deltaTime;
        door.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
        yield return null;
    }

    door.transform.position = targetPosition;}
}