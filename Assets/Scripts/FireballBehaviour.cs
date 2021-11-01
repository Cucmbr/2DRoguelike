using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    public float projectileSpeed = 0.06f;
    public Vector2 _direction;
    public GameObject _impPrefab;

    private void Start()
    {
        StartCoroutine(DestroyFireball());
    }

    private void FixedUpdate()
    {
        transform.Translate(_direction * projectileSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            //отскок от стены под тем же углом
            Vector2 inDirection = _direction;
            Vector2 inNormal = collision.contacts[0].normal;
            _direction = Vector2.Reflect(inDirection, inNormal);
        }
        if (collision.gameObject.CompareTag("Player")) //Если Fireball наткнулся на Player...
        {
            collision.gameObject.GetComponent<PlayerScript>().CurrentHealth -= _impPrefab.GetComponent<EnemyClass>().Damage;//...то нанести урон, равный показателю урона Imp
            Destroy(gameObject);
            if (collision.gameObject.GetComponent<PlayerScript>().CurrentHealth <= 0)
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
