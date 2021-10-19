using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    public float projectileSpeed = 0.06f;
    public Vector2 _direction;

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
            Vector2 inDirection = _direction;
            Vector2 inNormal = collision.contacts[0].normal;
            _direction = Vector2.Reflect(inDirection, inNormal);
        }
    }

    IEnumerator DestroyFireball()
    {
        yield return new WaitForSeconds(8f);

        Destroy(gameObject);
    }
}
