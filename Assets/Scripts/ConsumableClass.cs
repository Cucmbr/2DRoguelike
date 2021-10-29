using UnityEngine;
using System.Collections;

public class ConsumableClass : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;
    public int coins;

    public float MovementSpeed;
    public float AtackSpeed;

    public float Damage;
    public float CriticalChanse;
    public float CriticalMultiply;

    private bool isLooting = false;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AutoLutingWawe"))
        {
            StartCoroutine(AutoLutting());
        }
    }
    IEnumerator AutoLutting()
    {
        yield return new WaitForSeconds(0);
        transform.position = Vector3.Slerp(transform.position, GameObject.Find("Player").transform.position, 0.05f);
        StartCoroutine(AutoLutting());
    }

}
