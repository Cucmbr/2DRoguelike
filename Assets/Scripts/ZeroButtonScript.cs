using UnityEngine;
using UnityEngine.UI;

public class ZeroButtonScript : MonoBehaviour
{
    void Awake()
    {
        transform.GetComponent<Button>().onClick.AddListener(GameObject.Find("Player").GetComponent<PlayerScript>().ChangeWeapon);
    }
}
