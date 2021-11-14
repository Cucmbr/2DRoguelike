using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour
{
    public float maxHealth = 10;
    public float currentHealth = 10;
    public int coins;

    public float movementSpeed = 1f;
    private float newMS;
    private float attackSpeed = 1f;
    private float attackDelay = 0.5f;
    public float damage;
    private float criticalChanse;
    private float criticalMultiply;

    private float fill = 1;

    Text hpText;

    public WeaponClass[] equipedWeapon = new WeaponClass[2];
    [SerializeField] private GameObject arrow;
    public GameObject[] buttons;
    [SerializeField] private GameObject[] prefabButtons;
    private int currentRange = 0;


    private WeaponClass currentWeapon;
    [SerializeField] private Vector3[] weaponPos;
    [SerializeField] private Vector3[] weaponScale;
    [SerializeField] private GameObject[] startWeapon;
    private Image[] hpBar = new Image[2];

    private GameObject objectForInteraction;

    private GameObject currentButton;
    private GameObject drowingButton;

    public bool isWall;

    private List<ArtifactClass> equipedArtifacts = new List<ArtifactClass>(0);

    private float atackTime = 0;
    private Coroutine lastRoutine = null;
    private bool isAttack = false;
    private bool isLastAttack = false;
    private bool canStop = false;
    private int lastWeapon;
    public float additionalDamage;

    private void Awake()
    {
        //Инициализация необходимых переменных игрок
        prefabButtons = GetComponent<PlayerScript>().buttons;
        prefabButtons[0].GetComponent<Button>().onClick = GameObject.Find("ZeroButton").GetComponent<Button>().onClick;
        prefabButtons[1].GetComponent<Button>().onClick = GameObject.Find("FirstButton").transform.GetComponent<Button>().onClick;
        prefabButtons[2].GetComponent<Button>().onClick = GameObject.Find("SecondButton").transform.GetComponent<Button>().onClick;
        GetComponent<PlayerScript>().currentButton = prefabButtons[0];
        equipedWeapon[0] = startWeapon[0].GetComponent<WeaponClass>();
        equipedWeapon[1] = startWeapon[1].GetComponent<WeaponClass>();

        hpBar[0] = GameObject.Find("HpBarBack").GetComponent<Image>();
        hpBar[1] = GameObject.Find("HpBar").GetComponent<Image>();

        equipedWeapon[0].weaponDamage = startWeapon[0].GetComponent<WeaponClass>().weaponDamage;
        equipedWeapon[0].weaponSprite = startWeapon[0].GetComponent<WeaponClass>().weaponSprite;

        equipedWeapon[1].weaponDamage = startWeapon[1].GetComponent<WeaponClass>().weaponDamage;
        equipedWeapon[1].weaponSprite = startWeapon[1].GetComponent<WeaponClass>().weaponSprite;

        currentWeapon = equipedWeapon[0];

        hpText = GameObject.Find("HpText").GetComponent<Text>();

        StartCoroutine(StartWeapPos());


        transform.GetChild(7).GetChild(0).GetComponent<SpriteRenderer>().sprite = currentWeapon.weaponSprite[0];
        drowingButton = GameObject.Find("ChangeWeapon");
    }

    IEnumerator StartWeapPos()
    {
        yield return new WaitForSeconds(0.5f);

        var pos = new Vector3(0, 0, 0);
        transform.GetChild(0).transform.localPosition = pos;
    }

    private void FixedUpdate()
    {
        //Отрисовка текущей кнопки
        drowingButton.GetComponent<Image>().sprite = currentButton.GetComponent<Image>().sprite;
        drowingButton.GetComponent<Button>().onClick = currentButton.GetComponent<Button>().onClick;

        //Отрисовка текущего оружия
        currentWeapon = equipedWeapon[currentRange];
        transform.GetChild(7).GetChild(0).GetComponent<SpriteRenderer>().sprite = currentWeapon.weaponSprite[0];

        //Отрисовка текущих очков здоровья
        fill = currentHealth / maxHealth;
        hpBar[1].fillAmount = fill;
        hpText.text = currentHealth.ToString() + "/" + maxHealth.ToString();
    }

    public void ChangeWeapon()
    {
        if (!isAttack)
        {
            //Смена текущего оружия
            if (currentRange == 1)
            {
                transform.GetChild(7).GetChild(0).transform.localScale = weaponScale[0];
                transform.GetChild(7).GetChild(0).transform.localPosition = weaponPos[0];
                transform.GetChild(7).GetChild(0).transform.localRotation = GameObject.Find("ZeroButton").transform.rotation;
                currentRange = 0;
            }
            else if (currentRange == 0)
            {
                transform.GetChild(7).GetChild(0).transform.localScale = weaponScale[1];
                transform.GetChild(7).GetChild(0).transform.localPosition = weaponPos[1];
                transform.GetChild(7).GetChild(0).transform.localRotation = GameObject.Find("FirstButton").transform.rotation;
                currentRange = 1;
            }

            currentWeapon = equipedWeapon[currentRange];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Вхождение в радиус подбора
        if (collision.tag == "Weapon" || collision.tag == "Artifact")
        {
            currentButton = prefabButtons[1];
            objectForInteraction = collision.gameObject;
        }

        if (collision.tag == "Consumable")
        {
            Use(collision.gameObject.GetComponent<ConsumableClass>());
        }

        if (collision.tag == "Chest")
        {
            currentButton = prefabButtons[2];
            objectForInteraction = collision.gameObject;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Вхождение в радиус подбора
        if (collision.tag == "Weapon" || collision.tag == "Artifact")
        {
            currentButton = prefabButtons[0];
        }

        if (collision.tag == "Chest")
        {
            currentButton = prefabButtons[0];
        }
    }

    public void EquipClick()
    {
        if (objectForInteraction.GetComponent<WeaponClass>() != null)
            Equip(objectForInteraction.GetComponent<WeaponClass>());
        else
            Equip(objectForInteraction.GetComponent<ArtifactClass>());
    }

    public void OpenClick()
    {
        objectForInteraction.GetComponent<ChestScript>().Open();
    }

    private void Equip(WeaponClass weapon)
    {
        //На месте предидущего оружия появляется то,что было экипированно
        var dropedItem = Instantiate(equipedWeapon[weapon.range].GetComponent<WeaponClass>().selfPrefabObject); ;
        dropedItem.transform.position = transform.position;
        dropedItem.transform.localScale = new Vector3(1, 1, 1);
        dropedItem.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        dropedItem.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        WeaponClass cash;

        //Кэш объект для работы имеющейся системы. В них сохраняются последние используемые предметы.   
        cash = Instantiate(weapon.transform.GetComponent<WeaponClass>());

        //Перезапись кэш объектов
        if (weapon.transform.GetComponent<WeaponClass>().range == 0)
        {
            PlayerPrefs.SetInt("Last Melee Weapon", PlayerPrefs.GetInt("Last Melee Weapon") + 1);
            cash.name = "Last Melee Weapon";
        }
        else
        {
            PlayerPrefs.SetInt("Last Range Weapon", PlayerPrefs.GetInt("Last Range Weapon") + 1);
            cash.name = "Last Range Weapon";
        }

        cash.transform.SetParent(GameObject.Find("ServiceObjects(dd)").transform);

        if (PlayerPrefs.GetInt("Last Melee Weapon") >= 2 && weapon.transform.GetComponent<WeaponClass>().range == 0)
        {
            Destroy(GameObject.Find("Last Melee Weapon"));
        }

        if (PlayerPrefs.GetInt("Last Range Weapon") >= 2 && weapon.transform.GetComponent<WeaponClass>().range == 1)
        {
            Destroy(GameObject.Find("Last Range Weapon"));
        }

        Destroy(weapon.gameObject);
        cash.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        cash.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        equipedWeapon[weapon.range] = cash;
    }

    public void Equip(ArtifactClass artifact)
    {
        ArtifactClass cash;

        //Кэш объект для работы имеющейся системы. В них сохраняются последние используемые предметы.   
        cash = Instantiate(artifact.transform.GetComponent<ArtifactClass>());
        cash.transform.SetParent(GameObject.Find("ServiceObjects(dd)").transform);
        cash.GetComponent<Collider2D>().enabled = false;
        cash.transform.localScale = new Vector3(1, 1, 1);
        cash.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        cash.gameObject.GetComponent<CircleCollider2D>().enabled = false;

        equipedArtifacts.Add(cash);
        damage += artifact.GetComponent<ArtifactClass>().damage;
        maxHealth += artifact.GetComponent<ArtifactClass>().maxHealth;
        
        if (currentHealth + artifact.GetComponent<ArtifactClass>().currentHealth > maxHealth)
            currentHealth = maxHealth;
        else
            currentHealth = currentHealth + artifact.GetComponent<ArtifactClass>().currentHealth;
        
        movementSpeed += artifact.GetComponent<ArtifactClass>().movementSpeed;
        attackSpeed += artifact.GetComponent<ArtifactClass>().atackSpeed;
        criticalChanse += artifact.GetComponent<ArtifactClass>().criticalChanse;
        criticalMultiply += artifact.GetComponent<ArtifactClass>().criticalMultiply;
        coins += artifact.GetComponent<ArtifactClass>().coins;

        Destroy(artifact.gameObject);
    }

    public void Use(ConsumableClass consumable)
    {
        damage += consumable.GetComponent<ConsumableClass>().damage;
        maxHealth += consumable.GetComponent<ConsumableClass>().maxHealth;

        if (currentHealth + consumable.GetComponent<ConsumableClass>().currentHealth > maxHealth)
            currentHealth = maxHealth;
        else
            currentHealth = currentHealth + consumable.GetComponent<ConsumableClass>().currentHealth;
        
        movementSpeed += consumable.GetComponent<ConsumableClass>().movementSpeed;
        attackSpeed += consumable.GetComponent<ConsumableClass>().atackSpeed;
        criticalChanse += consumable.GetComponent<ConsumableClass>().criticalChanse;
        criticalMultiply += consumable.GetComponent<ConsumableClass>().criticalMultiply;
        coins += consumable.GetComponent<ConsumableClass>().coins;

        Destroy(consumable.gameObject);
    }

    public void Attack()
    {
        if (!isLastAttack)
        {
            newMS = movementSpeed;
            if (currentRange == 0)
            {
                //Атака ближнего боя
                transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                lastRoutine = StartCoroutine(MeleeAttack());
                lastWeapon = 0;
            }
            else if (currentRange == 1)
            {
                //Атака дальнего боя
                var scale = transform.GetChild(0).GetChild(3).transform.localScale;
                scale.y = 1;
                transform.GetChild(0).GetChild(3).transform.localScale = scale;
                transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                lastRoutine = StartCoroutine(RangeAttack());
                lastWeapon = 1;
            }

            isAttack = true;
            canStop = true;
        }
    }

    IEnumerator MeleeAttack()
    {
        yield return new WaitForSeconds(0);

        atackTime += Time.deltaTime*attackSpeed;

        if (atackTime >= 0)
        {
            movementSpeed = newMS - newMS / 10;
            transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            additionalDamage = 1;
        }

        if (atackTime >= 0.8f)
        {
            movementSpeed = newMS - newMS / 3;
            transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            additionalDamage = 2;
        }

        if (atackTime >= 1.3f)
        {
            movementSpeed = newMS - newMS / 2;
            transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            additionalDamage = 3;
        }

        lastRoutine = StartCoroutine(MeleeAttack());
    }

    IEnumerator RangeAttack()
    {
        yield return new WaitForSeconds(0);

        var scale = transform.GetChild(0).GetChild(3).transform.localScale;

        if (scale.y < 4.5f)
            scale.y += Time.deltaTime * attackSpeed * 1.3f;

        transform.GetChild(0).GetChild(3).transform.localScale = scale;

        if (atackTime >= 0)
        {
            movementSpeed = newMS - newMS / 10;
            transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
            transform.GetChild(7).GetChild(0).GetComponent<SpriteRenderer>().sprite = equipedWeapon[1].weaponSprite[0];
            transform.GetChild(7).GetChild(0).transform.localPosition = new Vector2(-0.156f, -0.126f);
            additionalDamage = 1;
        }

        atackTime = atackTime + Time.deltaTime * attackSpeed;

        if (atackTime >= 1)
        {
            movementSpeed = newMS - newMS / 3;
            transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
            transform.GetChild(7).GetChild(0).GetComponent<SpriteRenderer>().sprite = equipedWeapon[1].weaponSprite[1];
            transform.GetChild(7).GetChild(0).transform.localPosition = new Vector2(-0.564f, -0.013f);
            additionalDamage = 2;
        }

        if (atackTime >= 1.5f)
        {
            
            movementSpeed = newMS - newMS / 2;
            transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<SpriteRenderer>().color = Color.green;
            transform.GetChild(7).GetChild(0).GetComponent<SpriteRenderer>().sprite = equipedWeapon[1].weaponSprite[1];
            transform.GetChild(7).GetChild(0).transform.localPosition = new Vector2(-0.564f, -0.013f);
            additionalDamage = 3;
        }

        lastRoutine = StartCoroutine(RangeAttack());
    }

    public void StopAtack()
    {
        if (!isLastAttack && canStop)
        {
            movementSpeed = newMS;

            if (lastWeapon == 0 && !isWall)
            {
                var anim = transform.GetChild(0).GetComponent<Animation>();
                anim.Play("Sword Attack");
            }
            else if (lastWeapon == 1)
            {
                transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                var arrow = Instantiate(this.arrow, transform.position, transform.GetChild(0).rotation);
                arrow.GetComponent<Arrowscript>().startPos = transform.position;
                arrow.GetComponent<Arrowscript>().distance = transform.GetChild(0).GetChild(3).transform.localScale.y * 2.1f;
                arrow.GetComponent<Arrowscript>().damage = additionalDamage;
                transform.GetChild(7).GetChild(0).transform.localPosition = new Vector2(-0.156f, -0.126f);
            }
            else if (lastWeapon == 0 && isWall)
            {
                transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            }

            StopCoroutine(lastRoutine);
            StartCoroutine(NextAttack());
            atackTime = 0;
            isAttack = false;
            isLastAttack = true;    
            canStop = false;
        }

        StopCoroutine("TryToAttack");
    }

    IEnumerator NextAttack()
    {
        yield return new WaitForSeconds(attackDelay);

        isLastAttack = false;
        lastRoutine = null;
    }

    public void CallTryToAttack()
    {
        StartCoroutine("TryToAttack");
    }

    IEnumerator TryToAttack()
    {
        yield return new WaitForSeconds(0.001f);

        if (lastRoutine == null )
        {
            Attack();
        }
        else
        {
            StartCoroutine("TryToAttack");
        }
    }
}

