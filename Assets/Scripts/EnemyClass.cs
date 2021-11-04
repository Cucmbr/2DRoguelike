using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    //Хар-ки всех существующих противников
    public float MaxHP;
    public float CurentHP;
    public float Damage;
    bool death = false;

    public GameObject HPPickUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EquipedWeapon"))
        {
            CurentHP -= PlayerScript.Damage + PlayerScript.EquipedWeapon[0].GetComponent<WeaponClass>().Weapondamage + PlayerScript.AdditionalDamage;
            Debug.Log(PlayerScript.Damage + PlayerScript.EquipedWeapon[0].GetComponent<WeaponClass>().Weapondamage + PlayerScript.AdditionalDamage);
        }
        if (collision.CompareTag("Arrow"))
        {
            CurentHP -= PlayerScript.Damage+collision.GetComponent<Arrowscript>().Damage + PlayerScript.EquipedWeapon[1].GetComponent<WeaponClass>().Weapondamage;
            Debug.Log(PlayerScript.Damage + collision.GetComponent<Arrowscript>().Damage + PlayerScript.EquipedWeapon[1].GetComponent<WeaponClass>().Weapondamage);
            Destroy(collision.gameObject);
        }
        if (CurentHP <= 0 && death == false)
        {
            death = true;
            BSRoom.enemies -= 1;
            var pickUp = Instantiate(HPPickUp);
            pickUp.transform.position = transform.position;
            pickUp.transform.localScale = new Vector3(1, 1, 1);
            Destroy(gameObject);
        }
    }
}
