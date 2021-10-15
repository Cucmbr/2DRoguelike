using UnityEngine;

public class WeaponAtack : MonoBehaviour
{
    //Скрипт висящий на оружии
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyClass>().CurentHP -= PlayerScript.Damage + PlayerScript.EquipedWeapon[0].GetComponent<WeaponClass>().Weapondamage;
            if(collision.GetComponent<EnemyClass>().CurentHP <= 0)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
