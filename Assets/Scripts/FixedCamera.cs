using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    public Transform player;

    void FixedUpdate()
    {
        
        transform.position = player.position;
    }
}
