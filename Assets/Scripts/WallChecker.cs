using UnityEngine;

public class WallChecker : MonoBehaviour
{
    GameObject Player;
    private void Awake()
    {
        Player = transform.parent.transform.parent.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Player.GetComponent<PlayerScript>().isWall = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Player.GetComponent<PlayerScript>().isWall = false;
        }
    }
}
