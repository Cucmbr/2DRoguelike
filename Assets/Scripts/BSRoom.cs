using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSRoom : MonoBehaviour
{
    public bool check = false;
    public bool wallsDestroyed = false;
    public int enemies = 0;
    public int maxLimit = 10;
    public int minLimit = 4;
    public GameObject[] walls;
    public GameObject[] enemyTypes;
    public Transform[] enemySpawnPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && check == false)
        {
            foreach (GameObject wall in walls)
            {
                wall.gameObject.SetActive(true);
            }
            check = true;
            while (enemies < minLimit)
            {
                for (int i = 0; i < enemySpawnPoints.Length; i++)
                {
                    if (enemies < maxLimit)
                    {
                        int rand = Random.Range(0, 11);
                        if (rand < 6)
                        {
                            enemies += 1;
                            GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
                            Instantiate(enemyType, enemySpawnPoints[i].position, Quaternion.identity);
                            enemySpawnPoints[i].gameObject.SetActive(false);
                        }
                    }
                }
            }
            StartCoroutine(CheckEnemies());
            for (int i = 0; i < enemySpawnPoints.Length; i++)
            {
                enemySpawnPoints[i].gameObject.SetActive(false);
            }
        }
    }
    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => enemies == 0);
        DestroyWalls();
    }
    public void DestroyWalls()
    {
        foreach (GameObject wall in walls)
        {
            if (wall != null && wall.transform.childCount != 0)
            {
                wall.gameObject.SetActive(false);
            }
        }
        wallsDestroyed = true;
    }
    void SpawnMonsters()
    {
        if (check == true)
        {
            for (int i = 0; i < enemySpawnPoints.Length; i++)
            {
                int rand = Random.Range(0, 11);
                if (rand < 6)
                {
                    GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
                    Instantiate(enemyType, enemySpawnPoints[i].position, Quaternion.identity);
                    Destroy(enemySpawnPoints[i]);
                }
            }
        }
        //ок
    }
}