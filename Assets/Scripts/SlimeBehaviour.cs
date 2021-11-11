using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SlimeBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) //Если Dog наткнулся на Player...
        {
            collision.gameObject.GetComponent<PlayerScript>().CurrentHealth -= transform.parent.transform.GetChild(0).GetComponent<EnemyClass>().Damage;//...то нанести урон, равный показателю урона Dog
            transform.parent.transform.GetChild(0).GetComponent<Pathfinding.AIPath>().canMove = false;
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

        transform.parent.transform.GetChild(0).GetComponent<Pathfinding.AIPath>().canMove = true;
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
