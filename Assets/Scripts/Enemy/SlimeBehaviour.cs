using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SlimeBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerScript>().currentHealth -=
                transform.parent.transform.GetChild(0).GetComponent<EnemyClass>().damage;// то нанести урон, равный показателю урона Slime

            transform.parent.transform.GetChild(0).GetComponent<Pathfinding.AIPath>().canMove = false;
            GetComponent<CircleCollider2D>().enabled = false;
            
            StartCoroutine(AttackCooldown());
            if (collision.gameObject.GetComponent<PlayerScript>().currentHealth <= 0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position = transform.parent.transform.GetChild(0).transform.position;
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(1);

        transform.parent.transform.GetChild(0).GetComponent<Pathfinding.AIPath>().canMove = true;
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
