using UnityEngine;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour
{
    [Header("Характеристики")]
    public float MaxHealth = 10;
    public float CurrentHealth = 1;

    public float MovementSpeed;
    public float AtackSpeed;

    public float Damage;
    public float CriticalChanse;
    public float CriticalMultiply;

    private float fill = 1;

    public Text HpText;

    [SerializeField] Image[] HpBar;
    private void FixedUpdate()
    {
        fill = CurrentHealth / MaxHealth;
        HpBar[1].fillAmount = fill;
        HpText.text = CurrentHealth.ToString() + "/" + MaxHealth.ToString();
    }

}
