using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    public Transform player;

    void FixedUpdate()
    {
        var pos = player.position;
        pos.z = 0;
        transform.position = pos;
    }
}
