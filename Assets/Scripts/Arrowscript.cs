using UnityEngine;

public class Arrowscript : MonoBehaviour
{
    public float moveSpeed;
    void FixedUpdate()
    {
        transform.position += transform.GetChild(0).right * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
