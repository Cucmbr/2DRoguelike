using UnityEngine;

public class MeleeWeaponAtack : MonoBehaviour
{
    public GameObject HPPickUp;
    //Скрипт висящий на оружии
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Damage done" + (PlayerScript.Damage + PlayerScript.EquipedWeapon[0].GetComponent<WeaponClass>().Weapondamage).ToString());
            collision.GetComponent<EnemyClass>().CurentHP -= PlayerScript.Damage + PlayerScript.EquipedWeapon[0].GetComponent<WeaponClass>().Weapondamage;
            
            if(collision.GetComponent<EnemyClass>().CurentHP <= 0)
            {
                Destroy(collision.gameObject);
                Instantiate(HPPickUp, collision.gameObject.transform);
            }
        }
    }
}
