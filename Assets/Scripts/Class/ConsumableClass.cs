using UnityEngine;
using System.Collections;

public class ConsumableClass : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public int coins;

    public float movementSpeed;
    public float atackSpeed;

    public float damage;
    public float criticalChanse;
    public float criticalMultiply;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AutoLutingWawe"))
        {
            StartCoroutine(AutoLooting());
        }
    }
    IEnumerator AutoLooting()
    {
        //летит к игроку

        yield return new WaitForSeconds(0);

        transform.position = Vector3.Slerp(transform.position, GameObject.Find("Player").transform.position, 0.05f);
        StartCoroutine(AutoLooting());
    }
}
