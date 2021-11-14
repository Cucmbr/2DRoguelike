using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 0.06f;
    public Vector2 direction;
    [SerializeField] private GameObject impPrefab;

    private void Start()
    {
        StartCoroutine(DestroyFireball());
    }

    private void FixedUpdate()
    {
        transform.Translate(direction * projectileSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            //отскок от стены под тем же углом
            Vector2 inDirection = direction;
            Vector2 inNormal = collision.contacts[0].normal;
            direction = Vector2.Reflect(inDirection, inNormal);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerScript>().currentHealth -= impPrefab.GetComponent<EnemyClass>().damage; //нанести урон, равный показателю урона Imp
            Destroy(gameObject);

            if (collision.gameObject.GetComponent<PlayerScript>().currentHealth <= 0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    IEnumerator DestroyFireball()
    {
        yield return new WaitForSeconds(8f);

        Destroy(gameObject);
    }
}
