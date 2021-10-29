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
            CurentHP -= PlayerScript.Damage + PlayerScript.EquipedWeapon[0].GetComponent<WeaponClass>().Weapondamage;
        }
        if (collision.CompareTag("Arrow"))
        {
            CurentHP -= PlayerScript.Damage + PlayerScript.EquipedWeapon[1].GetComponent<WeaponClass>().Weapondamage;
            Destroy(collision.gameObject);
        }
        if (CurentHP <= 0)
        {
            var pickUp = Instantiate(HPPickUp);
            pickUp.transform.position = transform.position;
            pickUp.transform.localScale = new Vector3(1, 1, 1);
            Destroy(gameObject);
        }
    }
}
