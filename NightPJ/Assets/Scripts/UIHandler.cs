using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIHandler : MonoBehaviour
{
    public TextMeshProUGUI enemyCountText;
    public int calcEnemyCount = 10;
    public static UIHandler instance;

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
        }
    }

    public void UpdateEnemyCount()
    {
        enemyCountText.text = calcEnemyCount.ToString();
    }
}
