using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class PlayerScript : MonoBehaviour
{
    static public float MaxHealth = 10;
    public float CurrentHealth = 10;
    public int coins;

    static public float MovementSpeed = 0.15f;
    float newMS;
    static public float AtackSpeed;

    static public float Damage;
    static public float CriticalChanse;
    static public float CriticalMultiply;

    private float fill = 1;

    Text HpText;

    static public WeaponClass[] EquipedWeapon = new WeaponClass[2];
    public GameObject Arrow;
    public GameObject[] Buttons;
    static public GameObject[] _Buttons;
    static public int ButtonCounter = 0;
    public int CurrentRange = 0;


    public WeaponClass CurrentWeapon;
    public Vector3[] WeaponPos;
    public Vector3[] WeaponScale;
    public GameObject[] StartWeapon;
    [SerializeField] Image[] HpBar;

    GameObject EmptyForEquip;

    public GameObject CurrentButton;
    public GameObject DrowingButton;

    public bool isWall;

    static public List<ArtifactClass> EquipedArtifacts = new List<ArtifactClass>(0);

    private float atackTime = 0;
    Coroutine lastRoutine = null;
    public bool isAttack = false;
    public bool isLastAttack = false;
    public bool canStop = false;
    int lastWeap;
    static public float AdditionalDamage;
    private void Awake()
    {
        //Инициализация необходимых переменных игрок
        _Buttons = GameObject.Find("Player").GetComponent<PlayerScript>().Buttons;
        _Buttons[0].GetComponent<Button>().onClick = GameObject.Find("ZeroButton").GetComponent<Button>().onClick;
        _Buttons[1].GetComponent<Button>().onClick = GameObject.Find("FirstButton").transform.GetComponent<Button>().onClick;
        _Buttons[2].GetComponent<Button>().onClick = GameObject.Find("SecondButton").transform.GetComponent<Button>().onClick;
        GameObject.Find("Player").GetComponent<PlayerScript>().CurrentButton = PlayerScript._Buttons[0];
        EquipedWeapon[0] = StartWeapon[0].GetComponent<WeaponClass>();
        EquipedWeapon[1] = StartWeapon[1].GetComponent<WeaponClass>();

        EquipedWeapon[0].Weapondamage = StartWeapon[0].GetComponent<WeaponClass>().Weapondamage;
        EquipedWeapon[0].WeaponSprite = StartWeapon[0].GetComponent<WeaponClass>().WeaponSprite;

        EquipedWeapon[1].Weapondamage = StartWeapon[1].GetComponent<WeaponClass>().Weapondamage;
        EquipedWeapon[1].WeaponSprite = StartWeapon[1].GetComponent<WeaponClass>().WeaponSprite;

        CurrentWeapon = EquipedWeapon[0];

        HpText = GameObject.Find("HpText").GetComponent<Text>();

        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = CurrentWeapon.WeaponSprite[0];
        DrowingButton = GameObject.Find("ChangeWeapon");
    }


    private void FixedUpdate()
    {
        //Отрисовка текущей кнопки
        DrowingButton.GetComponent<Image>().sprite = CurrentButton.GetComponent<Image>().sprite;
        DrowingButton.GetComponent<Button>().onClick = CurrentButton.GetComponent<Button>().onClick;

        //Отрисовка текущего оружия
        CurrentWeapon = EquipedWeapon[CurrentRange];
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = CurrentWeapon.WeaponSprite[0];

        //Отрисовка текущих очков здоровья
        fill = CurrentHealth / MaxHealth;
        HpBar[1].fillAmount = fill;
        HpText.text = CurrentHealth.ToString() + "/" + MaxHealth.ToString();


    }

    public void ChangeWeapon()
    {
        if (!isAttack)
        {
            //Смена текущего оружия
            if (CurrentRange == 1)
            {
                transform.GetChild(0).transform.localScale = WeaponScale[0];
                transform.GetChild(0).transform.localPosition = WeaponPos[0];
                transform.GetChild(0).transform.localRotation = GameObject.Find("ZeroButton").transform.rotation;
                CurrentRange = 0;
            }
            else if (CurrentRange == 0)
            {
                transform.GetChild(0).transform.localScale = WeaponScale[1];
                transform.GetChild(0).transform.localPosition = WeaponPos[1];
                transform.GetChild(0).transform.localRotation = GameObject.Find("FirstButton").transform.rotation;
                CurrentRange = 1;
            }
            CurrentWeapon = EquipedWeapon[CurrentRange];
        }
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
        Cash.transform.SetParent(GameObject.Find("ServiceObjects(dd)").transform);
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
        Cash.transform.SetParent(GameObject.Find("ServiceObjects(dd)").transform);
        Cash.GetComponent<Collider2D>().enabled = false;
        Cash.transform.localScale = new Vector3(1, 1, 1);
        Cash.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Cash.gameObject.GetComponent<CircleCollider2D>().enabled = false;

        EquipedArtifacts.Add(Cash);
        Damage += item.GetComponent<ArtifactClass>().Damage;
        MaxHealth += item.GetComponent<ArtifactClass>().MaxHealth;
        if (CurrentHealth + item.GetComponent<ConsumableClass>().CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;
        else
            CurrentHealth = CurrentHealth + item.GetComponent<ConsumableClass>().CurrentHealth;
        MovementSpeed += item.GetComponent<ArtifactClass>().MovementSpeed;
        AtackSpeed += item.GetComponent<ArtifactClass>().AtackSpeed;
        CriticalChanse += item.GetComponent<ArtifactClass>().CriticalChanse;
        CriticalMultiply += item.GetComponent<ArtifactClass>().CriticalMultiply;
        coins += item.GetComponent<ArtifactClass>().coins;


        Destroy(item.gameObject);
    }
    public void Use(ConsumableClass item)
    {
        Damage += item.GetComponent<ConsumableClass>().Damage;
        MaxHealth += item.GetComponent<ConsumableClass>().MaxHealth;
        if (CurrentHealth + item.GetComponent<ConsumableClass>().CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;
        else
            CurrentHealth = CurrentHealth + item.GetComponent<ConsumableClass>().CurrentHealth;
        MovementSpeed += item.GetComponent<ConsumableClass>().MovementSpeed;
        AtackSpeed += item.GetComponent<ConsumableClass>().AtackSpeed;
        CriticalChanse += item.GetComponent<ConsumableClass>().CriticalChanse;
        CriticalMultiply += item.GetComponent<ConsumableClass>().CriticalMultiply;
        coins += item.GetComponent<ConsumableClass>().coins;

        Destroy(item.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Вхождение в радиус подбора
        if (collision.tag == "Weapon" || collision.tag == "Artifact")
        {
            CurrentButton = _Buttons[1];
            EmptyForEquip = collision.gameObject;
        }
        if (collision.tag == "Consumable")
        {
            Use(collision.gameObject.GetComponent<ConsumableClass>());
        }
        if (collision.tag == "Chest")
        {
            CurrentButton = _Buttons[2];
            EmptyForEquip = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Вхождение в радиус подбора
        if (collision.tag == "Weapon" || collision.tag == "Artifact")
        {
            CurrentButton = _Buttons[0];
        }
        if (collision.tag == "Chest")
        {
            CurrentButton = _Buttons[0];
        }
    }
    public void EquipClick()
    {
        if (EmptyForEquip.GetComponent<WeaponClass>() != null)
            Equip(EmptyForEquip.GetComponent<WeaponClass>());
        else
            Equip(EmptyForEquip.GetComponent<ArtifactClass>());
    }
    public void Atack()
    {
        if (!isLastAttack)
        {
            newMS = MovementSpeed;
            if (CurrentRange == 0)
            {

                //Атака ближнего боя
                transform.GetChild(0).GetChild(0).gameObject.active = false;
                transform.GetChild(0).GetChild(1).gameObject.active = false;
                transform.GetChild(0).GetChild(2).gameObject.active = false;
                lastRoutine = StartCoroutine(MeleeAttack());
                lastWeap = 0;
            }
            else if (CurrentRange == 1)
            {
                //Атака дальнего боя
                var scale = transform.GetChild(0).GetChild(3).transform.localScale;
                scale.y = 1;
                transform.GetChild(0).GetChild(3).transform.localScale = scale;
                transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                lastRoutine = StartCoroutine(RangeAttack());
                lastWeap = 1;
            }
            isAttack = true;
            canStop = true;
        }
    }
    public void OpenClick()
    {
        EmptyForEquip.GetComponent<ChestScript>().Open();
    }
    IEnumerator MeleeAttack()
    {

        yield return new WaitForSeconds(0);
        atackTime = atackTime + Time.deltaTime*1.3f;
        if (atackTime >= 0)
        {
            MovementSpeed = newMS - newMS / 10;
            transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(0).GetChild(0).gameObject.active = true;
            AdditionalDamage = 1;
        }
        if (atackTime >= 1)
        {
            MovementSpeed = newMS - newMS / 3;
            transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(0).GetChild(1).gameObject.active = true;
            AdditionalDamage = 2;
        }
        if (atackTime >= 1.5f)
        {
            MovementSpeed = newMS - newMS / 2;
            transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(0).GetChild(2).gameObject.active = true;
            AdditionalDamage = 3;
        }
        lastRoutine = StartCoroutine(MeleeAttack());

    }
    IEnumerator RangeAttack()
    {
        yield return new WaitForSeconds(0);
        var scale = transform.GetChild(0).GetChild(3).transform.localScale;
        if (scale.y < 4.5f)
            scale.y += Time.deltaTime * 1.3f;
        transform.GetChild(0).GetChild(3).transform.localScale = scale;
        if (atackTime >= 0)
        {
            MovementSpeed = newMS - newMS / 10;
            transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = EquipedWeapon[1].WeaponSprite[0];
            AdditionalDamage = 1;
        }
        atackTime = atackTime + Time.deltaTime;
        if (atackTime >= 1)
        {
            MovementSpeed = newMS - newMS / 3;
            transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = EquipedWeapon[1].WeaponSprite[1];
            AdditionalDamage = 2;
        }
        if (atackTime >= 1.5f)
        {
            MovementSpeed = newMS - newMS / 2;
            transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = EquipedWeapon[1].WeaponSprite[2];
            AdditionalDamage = 3;
        }
        lastRoutine = StartCoroutine(RangeAttack());

    }
    public void StopAtack()
    {
        if (!isLastAttack && canStop)
        {
            MovementSpeed = newMS;
            if (lastWeap == 0 && !isWall)
            {
                var anim = transform.GetChild(0).GetComponent<Animation>();
                anim.Play("Sword Attack");
            }
            else if (lastWeap == 1)
            {
                transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                var _arrow = Instantiate(Arrow, transform.position, transform.rotation);
                _arrow.GetComponent<Arrowscript>().startPos = transform.position;
                _arrow.GetComponent<Arrowscript>().distance = transform.GetChild(0).GetChild(3).transform.localScale.y * 2.1f;
                _arrow.GetComponent<Arrowscript>().Damage = AdditionalDamage;
            }
            else if (lastWeap == 0 && isWall)
            {
                transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(0).GetChild(0).gameObject.active = false;
                transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(0).GetChild(1).gameObject.active = false;
                transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(0).GetChild(2).gameObject.active = false;
            }
            StopCoroutine(lastRoutine);
            StartCoroutine(NextAttack());
            atackTime = 0;
            isAttack = false;
            isLastAttack = true;
            canStop = false;
        }
    }
    IEnumerator NextAttack()
    {
        yield return new WaitForSeconds(0.5f);
        isLastAttack = false;
    }
}

