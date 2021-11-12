using UnityEngine;
using TMPro;

public class EnemyClass : MonoBehaviour
{
    //Хар-ки всех существующих противников
    [SerializeField] private float maxHP;
    public float curentHP;
    public float damage;

    [SerializeField] private GameObject doneDamageText;
    [SerializeField] private GameObject HPPickUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EquipedWeapon"))
        {
            var player = collision.transform.parent.GetComponent<PlayerScript>();

            curentHP -= player.damage + player.equipedWeapon[0].GetComponent<WeaponClass>().weaponDamage;
            curentHP -= player.damage + player.equipedWeapon[0].
                GetComponent<WeaponClass>().weaponDamage + player.additionalDamage;

            var text = Instantiate(doneDamageText, transform.position, transform.rotation);
            text.transform.GetChild(0).GetComponent<TextMeshPro>().
                SetText((player.damage + player.equipedWeapon[0].
                GetComponent<WeaponClass>().weaponDamage + player.additionalDamage).ToString());
            
            var anim = text.transform.GetChild(0).GetComponent<Animation>();
            switch (Random.Range(0, 2))
            {
                case 0:
                    anim.Play("DamageFly");
                    break;
                case 1:
                    anim.Play("DamageFly 1");
                    break;
            }            
        }

        if (collision.CompareTag("Arrow"))
        {
            var player = GameObject.FindWithTag("Player").transform.GetComponent<PlayerScript>();

            curentHP -= player.damage + player.equipedWeapon[1].GetComponent<WeaponClass>().weaponDamage;
            curentHP -= player.damage+collision.
                GetComponent<Arrowscript>().damage + player.equipedWeapon[1].GetComponent<WeaponClass>().weaponDamage;
            
            var text = Instantiate(doneDamageText, transform.position, transform.rotation);
            text.transform.transform.GetChild(0).GetComponent<TextMeshPro>().
                SetText((player.damage + collision.
                GetComponent<Arrowscript>().damage + player.equipedWeapon[1].
                GetComponent<WeaponClass>().weaponDamage).ToString());
            
            var anim = text.transform.GetChild(0).GetComponent<Animation>();
            switch (Random.Range(0, 2))
            {
                case 0:
                    anim.Play("DamageFly");
                    break;
                case 1:
                    anim.Play("DamageFly 1");
                    break;
            }
            Destroy(collision.gameObject);
        }

        if (curentHP <= 0)
        {
            BSRoom.enemies -= 1;
            
            var pickUp = Instantiate(HPPickUp);
            pickUp.transform.position = transform.position;
            pickUp.transform.localScale = new Vector3(1, 1, 1);
            Destroy(gameObject);
            
            if (transform.parent != null)
                Destroy(transform.parent.gameObject);
            else
                Destroy(gameObject);
        }
    }
}
