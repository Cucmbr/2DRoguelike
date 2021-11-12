using UnityEngine;

public class Arrowscript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public float distance;
    public Vector3 startPos;
    public float damage;
    void FixedUpdate()
    {
        //����� �������� ���������
        if (Vector3.Distance(startPos, transform.position) < distance)
            transform.position += transform.GetChild(0).right * moveSpeed * Time.deltaTime;
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //��� ������� �� ������, ������������
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
