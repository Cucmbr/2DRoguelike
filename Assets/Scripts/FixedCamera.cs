using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    public Transform player;

    void FixedUpdate()
    {
        var pos = player.position;
        pos.z = -1;
        transform.position = pos;
    }
}
