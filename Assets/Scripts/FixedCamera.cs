using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    void FixedUpdate()
    {
        var pos = player.position;
        pos.z = -1;
        transform.position = pos;
    }
}
