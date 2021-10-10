using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    bool NearPlayer = false;
    public void Equip()
    {
        //Если игрок подошел к оружию
        if (NearPlayer)
        {
            //На месте предидущего оружия появляется то,что было экипированно
            var _item = Instantiate(PlayerScript.EquipedWeapon[transform.GetComponent<WeaponClass>().range].GetComponent<WeaponClass>().Object); ;
            _item.transform.SetParent(GameObject.Find("Canvas").transform);
            _item.transform.position = transform.position;
            _item.transform.localScale = new Vector3(1, 1, 1);
            _item.GetComponent<Image>().enabled = true;
            _item.GetComponent<Button>().enabled = true;
            WeaponClass Class;

            //Кэш объект для работы имеющейся системы. В них сохраняются последние используемые предметы.   
            Class = Instantiate(transform.GetComponent<WeaponClass>());

            //Перезапись кэш объектов
            if (transform.GetComponent<WeaponClass>().range == 0)
            {
                PlayerPrefs.SetInt("Last Melee Weapon", PlayerPrefs.GetInt("Last Melee Weapon") + 1);
                Class.name = "Last Melee Weapon";
                Class.transform.SetParent(GameObject.Find("ServiceObjects").transform);
            }
            else
            {
                PlayerPrefs.SetInt("Last Range Weapon", PlayerPrefs.GetInt("Last Range Weapon") + 1);
                Class.name = "Last Range Weapon";
                Class.transform.SetParent(GameObject.Find("ServiceObjects").transform);
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
            DontDestroyOnLoad(Class);
        }
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Вхождение в радиус подбора
        if (collision.tag == "Player")
        {
            PlayerScript.CurrentButton = PlayerScript._Buttons[1];
            PlayerScript.CurrentButton.GetComponent<Button>().onClick = transform.GetComponent<Button>().onClick;
            NearPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Выход из радиуса подбора
        if (collision.tag == "Player")
        {
            PlayerScript.CurrentButton = PlayerScript._Buttons[0];
            NearPlayer = false;
        }
    }
}
