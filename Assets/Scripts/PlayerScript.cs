using UnityEngine;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour
{
    [Header("Характеристики")]
    public float MaxHealth = 10;
    public float CurrentHealth = 1;

    public float MovementSpeed;
    public float AtackSpeed;

    static public float Damage;
    public float CriticalChanse;
    public float CriticalMultiply;

    private float fill = 1;

    public Text HpText;
    public Text DmgText;

    static public WeaponClass[] EquipedWeapon = new WeaponClass[2];
    public GameObject[] Buttons;
    static public GameObject[] _Buttons; // Ne sdelano
    static public int ButtonCounter = 0;
    public int CurrentRange = 0;


    public WeaponClass CurrentWeapon;
    public GameObject[] StartWeapon;
    [SerializeField] Image[] HpBar;

    

    static public GameObject CurrentButton; 
    public GameObject DrowingButton;
    private void Awake()
    {    
        //Инициализация необходимых переменных игрока
        _Buttons = Buttons;
        _Buttons[0].GetComponent<Button>().onClick = GameObject.Find("ZeroButton").GetComponent<Button>().onClick;
        CurrentButton = _Buttons[ButtonCounter];
        EquipedWeapon[0] = StartWeapon[0].GetComponent<WeaponClass>();
        EquipedWeapon[1] = StartWeapon[1].GetComponent<WeaponClass>();

        EquipedWeapon[0].Weapondamage = StartWeapon[0].GetComponent<WeaponClass>().Weapondamage;
        EquipedWeapon[0].WeaponSprite = StartWeapon[0].GetComponent<WeaponClass>().WeaponSprite;

        EquipedWeapon[1].Weapondamage = StartWeapon[1].GetComponent<WeaponClass>().Weapondamage;
        EquipedWeapon[1].WeaponSprite = StartWeapon[1].GetComponent<WeaponClass>().WeaponSprite;

        CurrentWeapon = EquipedWeapon[0];

        transform.GetChild(0).GetComponent<Image>().sprite = CurrentWeapon.WeaponSprite;
    }


    private void FixedUpdate()
    {
        //Отрисовка текущей кнопки
        DrowingButton.GetComponent<Image>().sprite = CurrentButton.GetComponent<Image>().sprite;
        DrowingButton.GetComponent<Button>().onClick = CurrentButton.GetComponent<Button>().onClick;

        //Отрисовка текущего оружия
        CurrentWeapon = EquipedWeapon[CurrentRange];
        transform.GetChild(0).GetComponent<Image>().sprite = CurrentWeapon.WeaponSprite;

        //Отрисовка текущих очков здоровья
        fill = CurrentHealth / MaxHealth;
        HpBar[1].fillAmount = fill;
        HpText.text = CurrentHealth.ToString() + "/" + MaxHealth.ToString();

        //Отрисовка урона текущего оружия
        DmgText.text = CurrentWeapon.Weapondamage.ToString();
    }

     public void ChangeWeapon()
    {
        //Смена текущего оружия
        if (CurrentRange == 1)
            CurrentRange = 0;
        else if (CurrentRange == 0)
            CurrentRange = 1;
        CurrentWeapon = EquipedWeapon[CurrentRange];
        transform.GetChild(0).GetComponent<Image>().sprite = CurrentWeapon.WeaponSprite;
    }

    public void MeleeAtack()
    {
        //Атака ближнего боя
        var anim = transform.GetChild(0).GetComponent<Animation>();
        anim.Play("АтакаСтартовымМечом");
    }

    
}

