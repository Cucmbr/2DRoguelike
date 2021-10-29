using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    //Хар-ки всех существующих противников
    public float MaxHP;
    public float CurentHP;
    public float Damage;
    public int maxcoins;

    public GameObject bronzeCoin;
    public GameObject silverCoin;
    public GameObject goldCoin;

    public GameObject HPPickUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EquipedWeapon"))
        {
            Debug.Log("Damage done " + (PlayerScript.Damage + PlayerScript.AdditionalDamage + PlayerScript.EquipedWeapon[0].GetComponent<WeaponClass>().Weapondamage).ToString());
            CurentHP -= PlayerScript.Damage + PlayerScript.AdditionalDamage + PlayerScript.EquipedWeapon[0].GetComponent<WeaponClass>().Weapondamage;
        }
        if (collision.CompareTag("Arrow"))
        {
            Debug.Log("Damage done " + (collision.GetComponent<Arrowscript>().Damage + PlayerScript.EquipedWeapon[1].GetComponent<WeaponClass>().Weapondamage).ToString());
            CurentHP -= PlayerScript.Damage + PlayerScript.EquipedWeapon[1].GetComponent<WeaponClass>().Weapondamage;
            Destroy(collision.gameObject);
        }
        if (CurentHP <= 0)
        {
            var pos = transform.position;
            var i = Random.RandomRange(0, 10);
            var coins = Random.RandomRange(0, maxcoins);
            Debug.Log("coins " + coins.ToString());
            int gold = coins / 10;
            coins %= 10;
            int silver = coins / 5;
            coins %= 5;
            for(int o = 1; o <= gold; o++)
            {
                pos = transform.position;
                pos.x += Random.Range(0, 2);
                pos.y += Random.Range(0, 2);
                Instantiate(goldCoin,pos,transform.rotation);
            }
             
            for (int o = 1; o <= silver; o++)
            {
                pos = transform.position;
                pos.x += Random.Range(0, 2);
                pos.y += Random.Range(0, 2);
                Instantiate(silverCoin, pos, transform.rotation);
            }
            for (int o = 1; o <= coins; o++)
            {
                pos = transform.position;
                pos.x += Random.Range(0, 2);
                pos.y += Random.Range(0, 2);
                Instantiate(bronzeCoin, pos, transform.rotation);
            }
            if (i > 5)
            {
                var pickUp = Instantiate(HPPickUp);
                pickUp.transform.position = transform.position;
                pickUp.transform.localScale = new Vector3(1, 1, 1);
            }
            Debug.Log("HpPickUpRange " + i.ToString());
            Destroy(gameObject);
        }
    }
}
