using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMoving : MonoBehaviour
{
    public GameObject PathFinding;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("U trigger"))
        {
            Vector2 pos = PathFinding.transform.position;
            pos.y -= 250;
            PathFinding.transform.position = pos;

        }
            
    }
}
