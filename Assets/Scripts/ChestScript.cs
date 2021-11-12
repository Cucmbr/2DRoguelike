using UnityEngine;

public class ChestScript : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private GameObject[] artifacts;
    [SerializeField] private bool isWeapon;
    public void Open()
    {
        //Открытие сундука с дропом вещи
        var item = isWeapon == false?
            Instantiate(artifacts[Random.Range(0, artifacts.Length)]):
            Instantiate(weapons[Random.Range(0, weapons.Length)].GetComponent<WeaponClass>().selfPrefabObject);
        item.transform.position = transform.position;
        item.transform.localScale = new Vector3(1, 1, 1);
        item.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        item.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        Destroy(gameObject);
    }
}
