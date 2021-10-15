using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    //���-�� ���� ������������ �����������
    public float MaxHP;
    public float CurentHP;
    public float Damage;

    public GameObject HPPickUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EquipedWeapon"))
        {
            Debug.Log("Damage done " + (PlayerScript.Damage + PlayerScript.EquipedWeapon[PlayerScript.CurrentRange].GetComponent<WeaponClass>().Weapondamage).ToString());
            CurentHP -= PlayerScript.Damage + PlayerScript.EquipedWeapon[PlayerScript.CurrentRange].GetComponent<WeaponClass>().Weapondamage;

            if (CurentHP <= 0)
            {
                var pickUp = Instantiate(HPPickUp);
                pickUp.transform.position = transform.position;
                pickUp.transform.localScale = new Vector3(1,1,1);
                Destroy(gameObject);
            }
        }
    }
}
