using UnityEngine;

public class ImpShooting : MonoBehaviour
{
    [SerializeField] private GameObject impProjectile;

    void Start()
    {
        InvokeRepeating("Shoot", 3f, 5f);
    }

    void Shoot()
    {
        GameObject shoot1 = Instantiate(impProjectile, transform.position, transform.rotation);
        FireballBehaviour sh1 = shoot1.GetComponent<FireballBehaviour>();
        sh1.direction = new Vector2(0.5f, 0.5f);

        GameObject shoot2 = Instantiate(impProjectile, transform.position, transform.rotation);
        FireballBehaviour sh2 = shoot2.GetComponent<FireballBehaviour>();
        sh2.direction = new Vector2(0.5f, -0.5f);

        GameObject shoot3 = Instantiate(impProjectile, transform.position, transform.rotation);
        FireballBehaviour sh3 = shoot3.GetComponent<FireballBehaviour>();
        sh3.direction = new Vector2(-0.5f, 0.5f);

        GameObject shoot4 = Instantiate(impProjectile, transform.position, transform.rotation);
        FireballBehaviour sh4 = shoot4.GetComponent<FireballBehaviour>();
        sh4.direction = new Vector2(-0.5f, -0.5f);
    }
}
