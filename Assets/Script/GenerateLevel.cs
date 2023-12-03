using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GenerateLevel : MonoBehaviour
{
    public static int level = 0;
    public GameObject[] enemyPrefab;
    public float spawnInterval = 2f;
    public float spawnDistance = 18f;
    private float timeElapsed = 0f;
    public float maxEnemyCount = 0;
    public GameObject Enemy;
    public static bool isCompleted;
    public GameObject Levelup;
    public GameObject healthBar;

    private void Start()
    {
        maxEnemyCount = Mathf.Pow(2, level);
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        Debug.Log("level" + level);
        Debug.Log("ENEMY" + maxEnemyCount);
        if (timeElapsed >= spawnInterval)
        {
            if (!isCompleted)
            {
                PlayerMovement.health = 10;
                healthBar.GetComponent<Slider>().value = 10;
                Levelup.SetActive(false);
                level++;
                if (level % 5 == 0)
                {
                    PlayerPrefs.SetInt("level",level);
                    PlayerPrefs.Save();
                }
                    maxEnemyCount = Mathf.Pow(2, level);

                for (int i = 0; i < maxEnemyCount; i++)
                {
                    SpawnEnemy();
                    if (i == maxEnemyCount - 1)
                    {
                        isCompleted = true;
                    }
                }
            }

            timeElapsed = 0f;
        }
    }

    void SpawnEnemy()
    {
        Vector3 playerPosition = Camera.main.transform.position;
        Vector2 spawnPosition = Random.insideUnitCircle * spawnDistance;
        Vector3 enemyPosition = new Vector3(spawnPosition.x, spawnPosition.y, 0f) + playerPosition;

        var enemy = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], enemyPosition, Quaternion.identity);
        enemy.transform.SetParent(Enemy.transform);
    }
}