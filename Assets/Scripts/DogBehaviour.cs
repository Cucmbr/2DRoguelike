using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DogBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) //���� Dog ��������� �� Player...
        {
            collision.gameObject.GetComponent<PlayerScript>().CurrentHealth -= GetComponent<EnemyClass>().Damage;//...�� ������� ����, ������ ���������� ����� Dog
            GetComponent<Pathfinding.AIPath>().canMove = false;
            GetComponent<CircleCollider2D>().enabled = false; 
            StartCoroutine(AttackCooldown());
            if (collision.gameObject.GetComponent<PlayerScript>().CurrentHealth <= 0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1);

        GetComponent<Pathfinding.AIPath>().canMove = true;
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
