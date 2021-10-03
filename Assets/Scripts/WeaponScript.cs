using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    public void Equip()
    {
        var _item = Instantiate(PlayerScript.EquipedWeapon[transform.GetComponent<WeaponClass>().range].GetComponent<WeaponClass>().Object); ;
        _item.transform.SetParent(GameObject.Find("Canvas").transform);
        _item.transform.position = transform.position;
        _item.transform.localScale = new Vector3(1, 1, 1);
        _item.GetComponent<Image>().enabled = true;
        _item.GetComponent<Button>().enabled = true;
        WeaponClass Class;
        
        Class = Instantiate(transform.GetComponent<WeaponClass>());
        
        if (transform.GetComponent<WeaponClass>().range == 0)
        {
            PlayerPrefs.SetInt("Last Melee Weapon", PlayerPrefs.GetInt("Last Melee Weapon") + 1);
            Class.name = "Last Melee Weapon";
        }
        else
        {
            PlayerPrefs.SetInt("Last Range Weapon", PlayerPrefs.GetInt("Last Range Weapon") + 1);
            Class.name = "Last Range Weapon";
        }
        PlayerScript.EquipedWeapon[transform.GetComponent<WeaponClass>().range] = Class;

        if (PlayerPrefs.GetInt("Last Melee Weapon") >= 2 && transform.GetComponent<WeaponClass>().range == 0)
        {
            Destroy(GameObject.Find("Last Melee Weapon"));
        }
        if (PlayerPrefs.GetInt("Last Range Weapon") >= 2 && transform.GetComponent<WeaponClass>().range == 1)
        {
            Destroy(GameObject.Find("Last Range Weapon"));
        }
        Destroy(this.gameObject);
       
        
        
    }
}
