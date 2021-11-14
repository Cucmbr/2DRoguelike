using UnityEngine;

public class WallChecker : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void Awake()
    {
        player = transform.parent.transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            player.GetComponent<PlayerScript>().isWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            player.GetComponent<PlayerScript>().isWall = false;
        }
    }
}
