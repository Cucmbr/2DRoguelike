using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public GameObject[] Weapons;
    public GameObject[] Artifacts;
    public bool isWeapon;
    public void Open()
    {

        var _item = isWeapon == false?Instantiate(Weapons[Random.RandomRange(0, Artifacts.Length)]): Instantiate(Weapons[Random.RandomRange(0, Weapons.Length)].GetComponent<WeaponClass>().Object);
        _item.transform.position = transform.position;
        _item.transform.localScale = new Vector3(1, 1, 1);
        _item.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        _item.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        Destroy(gameObject);
    }
}
