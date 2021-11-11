using UnityEngine;
using TMPro;
using System.Collections;
public class EnemyClass : MonoBehaviour
{
    //Хар-ки всех существующих противников
    public float MaxHP;
    public float CurentHP;
    public float Damage;
    bool death = false;
    public GameObject DoneDamageText;
    public GameObject HPPickUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EquipedWeapon"))
        {
            CurentHP -= PlayerScript.Damage + PlayerScript.EquipedWeapon[0].GetComponent<WeaponClass>().Weapondamage + PlayerScript.AdditionalDamage;
            Debug.Log(PlayerScript.Damage + PlayerScript.EquipedWeapon[0].GetComponent<WeaponClass>().Weapondamage + PlayerScript.AdditionalDamage);
            var text = Instantiate(DoneDamageText, transform.position, transform.rotation);
            text.transform.GetChild(0).GetComponent<TextMeshPro>().SetText((PlayerScript.Damage + PlayerScript.EquipedWeapon[0].GetComponent<WeaponClass>().Weapondamage + PlayerScript.AdditionalDamage).ToString());
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
            CurentHP -= PlayerScript.Damage+collision.GetComponent<Arrowscript>().Damage + PlayerScript.EquipedWeapon[1].GetComponent<WeaponClass>().Weapondamage;
            var text = Instantiate(DoneDamageText, transform.position, transform.rotation);
            text.transform.transform.GetChild(0).GetComponent<TextMeshPro>().SetText((PlayerScript.Damage + collision.GetComponent<Arrowscript>().Damage + PlayerScript.EquipedWeapon[1].GetComponent<WeaponClass>().Weapondamage).ToString());
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
        if (CurentHP <= 0 && death == false)
        {
            
            death = true;
            BSRoom.enemies -= 1;
            var pickUp = Instantiate(HPPickUp);
            pickUp.transform.position = transform.position;
            pickUp.transform.localScale = new Vector3(1, 1, 1);
            if (transform.parent != null)
                Destroy(transform.parent.gameObject);
            else
                Destroy(gameObject);
        }
    }
}
