using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadEnemyCounterUI : MonoBehaviour
{
    public static int deadEnemyNum=0;
    [SerializeField] private GameObject enemyCounterDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyCounterDisplay.GetComponent<Text>().text =Mathf.FloorToInt(deadEnemyNum).ToString("000");
    }
}
