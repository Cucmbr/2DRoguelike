using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnOfEnemies : MonoBehaviour
{
    public Vector2 _leftTopCornerOfSpawn;
    public Vector2 _rightBotCornerOfSpawn;
    Vector2 _finalPosToSpawn;
    float _randX, _randY;
    public int _amountOfEnemies;
    public GameObject[] _enemies;

    void Start()
    {
        for (int i = 0; i < _amountOfEnemies; i++)
        {
            _randX = Random.Range(_leftTopCornerOfSpawn.x, _rightBotCornerOfSpawn.x); // случайное значение для спавна по горизонтали
            _randY = Random.Range(_leftTopCornerOfSpawn.y, _rightBotCornerOfSpawn.y); // случайное значение для спавна по вертикали

            _finalPosToSpawn = new Vector2(_randX, _randY);

            GameObject _enemy = Instantiate(_enemies[Random.Range(0, _enemies.Length)], _finalPosToSpawn, Quaternion.identity);
            if (_enemy.CompareTag("Imp"))
                _enemy.GetComponent<ImpMovement>().player = GameObject.FindWithTag("Player").transform;
            else if (_enemy.CompareTag("Enemy"))
                _enemy.GetComponent<Pathfinding.AIDestinationSetter>().target = GameObject.FindWithTag("Player").transform;
        }
    }

}
