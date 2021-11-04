using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BSRoom : MonoBehaviour
{
    public bool check = false;
    public bool wallsDestroyed = false;
    public static int enemies = 0;
    public int maxLimit = 6;
    public int minLimit = 3;
    public GameObject[] walls;
    public GameObject[] enemyTypes;
    public Transform[] enemySpawnPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && check == false)
        {
            enemies = 0;
            foreach (GameObject wall in walls)
            {
                wall.gameObject.SetActive(true);
            }
            AstarPath.active.data.gridGraph.center = new Vector2(transform.position.x, transform.position.y);
            AstarPath.active.Scan();
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
                            GameObject _enemy = Instantiate(enemyType, enemySpawnPoints[i].position, Quaternion.identity);
                            if (_enemy.CompareTag("Imp"))
                                _enemy.GetComponent<ImpMovement>().player = GameObject.FindWithTag("Player").transform;
                            else if (_enemy.CompareTag("Enemy"))
                                _enemy.GetComponent<Pathfinding.AIDestinationSetter>().target = GameObject.FindWithTag("Player").transform;
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
        yield return new WaitUntil(() => enemies <= 0);
        DestroyWalls();
    }
    public void DestroyWalls()
    {
        foreach (GameObject wall in walls)
        {
            wall.gameObject.SetActive(false);
        }
        var anim = GameObject.Find("Player").transform.GetChild(1).GetComponent<Animation>();
        anim.Play("AutoLutingAnim");
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