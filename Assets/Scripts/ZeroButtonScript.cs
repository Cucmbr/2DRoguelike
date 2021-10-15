using UnityEngine;
using UnityEngine.UI;

public class ZeroButtonScript : MonoBehaviour
{
    void Awake()
    {
        transform.GetComponent<Button>().onClick.AddListener(PlayerScript.ChangeWeapon);
    }
}
