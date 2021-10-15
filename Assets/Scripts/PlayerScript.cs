using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class PlayerScript : MonoBehaviour
{
    [Header("Характеристики")]
    static public float MaxHealth = 10;
    static public float CurrentHealth = 1;

    static public float MovementSpeed;
    static public float AtackSpeed;

    static public float Damage;
    static public float CriticalChanse;
    static public float CriticalMultiply;

    private float fill = 1;

    public Text HpText;
    public Text DmgText;

    static public WeaponClass[] EquipedWeapon = new WeaponClass[2];
    public GameObject[] Buttons;
    static public GameObject[] _Buttons; // Ne sdelano
    static public int ButtonCounter = 0;
    static public int CurrentRange = 0;


    static public WeaponClass CurrentWeapon;
    public GameObject[] StartWeapon;
    [SerializeField] Image[] HpBar;

    GameObject EmtyForEquip;
    

    static public GameObject CurrentButton; 
    public GameObject DrowingButton;

    static public List<ArtifactClass> EquipedArtifacts = new List<ArtifactClass>(0);
    private void Awake()
    {    
        //Инициализация необходимых переменных игрока
        _Buttons = Buttons;
        _Buttons[0].GetComponent<Button>().onClick = GameObject.Find("ZeroButton").GetComponent<Button>().onClick;
        _Buttons[1].GetComponent<Button>().onClick = GameObject.Find("FirstButton").GetComponent<Button>().onClick;
        CurrentButton = _Buttons[ButtonCounter];
        EquipedWeapon[0] = StartWeapon[0].GetComponent<WeaponClass>();
        EquipedWeapon[1] = StartWeapon[1].GetComponent<WeaponClass>();

        EquipedWeapon[0].Weapondamage = StartWeapon[0].GetComponent<WeaponClass>().Weapondamage;
        EquipedWeapon[0].WeaponSprite = StartWeapon[0].GetComponent<WeaponClass>().WeaponSprite;

        EquipedWeapon[1].Weapondamage = StartWeapon[1].GetComponent<WeaponClass>().Weapondamage;
        EquipedWeapon[1].WeaponSprite = StartWeapon[1].GetComponent<WeaponClass>().WeaponSprite;

        CurrentWeapon = EquipedWeapon[0];

        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = CurrentWeapon.WeaponSprite;
    }


    private void FixedUpdate()
    {
        //Отрисовка текущей кнопки
        DrowingButton.GetComponent<Image>().sprite = CurrentButton.GetComponent<Image>().sprite;
        DrowingButton.GetComponent<Button>().onClick = CurrentButton.GetComponent<Button>().onClick;

        //Отрисовка текущего оружия
        CurrentWeapon = EquipedWeapon[CurrentRange];
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = CurrentWeapon.WeaponSprite;

        //Отрисовка текущих очков здоровья
        fill = CurrentHealth / MaxHealth;
        HpBar[1].fillAmount = fill;
        HpText.text = CurrentHealth.ToString() + "/" + MaxHealth.ToString();

        //Отрисовка урона текущего оружия
        DmgText.text = CurrentWeapon.Weapondamage.ToString();
    }

    static public void ChangeWeapon()
    {
        //Смена текущего оружия
        if (CurrentRange == 1)
            CurrentRange = 0;
        else if (CurrentRange == 0)
            CurrentRange = 1;
        CurrentWeapon = EquipedWeapon[CurrentRange];
    }

    public void MeleeAtack()
    {
        //Атака ближнего боя
        var anim = transform.GetChild(0).GetComponent<Animation>();
        anim.Play("АтакаСтартовымМечом");
    }

    private void Equip(WeaponClass item)
    {
        //На месте предидущего оружия появляется то,что было экипированно
        var _item = Instantiate(EquipedWeapon[item.range].GetComponent<WeaponClass>().Object); ;
        //_item.transform.SetParent(GameObject.Find("Canvas").transform);
        _item.transform.position = transform.position;
        _item.transform.localScale = new Vector3(1, 1, 1);
        _item.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        _item.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        WeaponClass Cash;

        //Кэш объект для работы имеющейся системы. В них сохраняются последние используемые предметы.   
        Cash = Instantiate(item.transform.GetComponent<WeaponClass>());
        //Перезапись кэш объектов
        if (item.transform.GetComponent<WeaponClass>().range == 0)
        {
            PlayerPrefs.SetInt("Last Melee Weapon", PlayerPrefs.GetInt("Last Melee Weapon") + 1);
            Cash.name = "Last Melee Weapon";
        }
        else
        {
            PlayerPrefs.SetInt("Last Range Weapon", PlayerPrefs.GetInt("Last Range Weapon") + 1);
            Cash.name = "Last Range Weapon"; 
        }
        Cash.transform.SetParent(GameObject.Find("ServiceObjects").transform);
        if (PlayerPrefs.GetInt("Last Melee Weapon") >= 2 && item.transform.GetComponent<WeaponClass>().range == 0)
        {
            Destroy(GameObject.Find("Last Melee Weapon"));
        }
        if (PlayerPrefs.GetInt("Last Range Weapon") >= 2 && item.transform.GetComponent<WeaponClass>().range == 1)
        {
            Destroy(GameObject.Find("Last Range Weapon"));
        }
        Destroy(item.gameObject);
        Cash.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Cash.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        EquipedWeapon[item.range] = Cash;
    }
    public void Equip(ArtifactClass item)
    {
        ArtifactClass Cash;

        //Кэш объект для работы имеющейся системы. В них сохраняются последние используемые предметы.   
        Cash = Instantiate(item.transform.GetComponent<ArtifactClass>());
        Cash.transform.SetParent(GameObject.Find("ServiceObjects").transform);
        Cash.GetComponent<Collider2D>().enabled = false;
        Cash.transform.localScale = new Vector3(1, 1, 1);
        Cash.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Cash.gameObject.GetComponent<CircleCollider2D>().enabled = false;

        EquipedArtifacts.Add(Cash);
        Damage += item.GetComponent<ArtifactClass>().Damage;
        MaxHealth += item.GetComponent<ArtifactClass>().MaxHealth;
        CurrentHealth += item.GetComponent<ArtifactClass>().CurrentHealth;
        MovementSpeed += item.GetComponent<ArtifactClass>().MovementSpeed;
        AtackSpeed += item.GetComponent<ArtifactClass>().AtackSpeed;
        CriticalChanse += item.GetComponent<ArtifactClass>().CriticalChanse;
        CriticalMultiply += item.GetComponent<ArtifactClass>().CriticalMultiply;
            
            
        Destroy(item.gameObject);
    }
    public void Use(ConsumableClass item)
    {
        Damage += item.GetComponent<ConsumableClass>().Damage;
        MaxHealth += item.GetComponent<ConsumableClass>().MaxHealth;
        CurrentHealth += item.GetComponent<ConsumableClass>().CurrentHealth;
        MovementSpeed += item.GetComponent<ConsumableClass>().MovementSpeed;
        AtackSpeed += item.GetComponent<ConsumableClass>().AtackSpeed;
        CriticalChanse += item.GetComponent<ConsumableClass>().CriticalChanse;
        CriticalMultiply += item.GetComponent<ConsumableClass>().CriticalMultiply;


        Destroy(item.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Вхождение в радиус подбора
        if (collision.tag == "Weapon" || collision.tag == "Artifact")
        {
            CurrentButton = _Buttons[1];
            EmtyForEquip = collision.gameObject;
        }
        if (collision.tag == "Consumable")
        {
            Use(collision.gameObject.GetComponent<ConsumableClass>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Вхождение в радиус подбора
        if (collision.tag == "Weapon" || collision.tag == "Artifact")
        {
            CurrentButton = _Buttons[0];
        }
    }
    public void EquipClick()
    {
        if(EmtyForEquip.GetComponent<WeaponClass>() != null)
            Equip(EmtyForEquip.GetComponent<WeaponClass>());
        else 
            Equip(EmtyForEquip.GetComponent<ArtifactClass>());
        
    }
}

