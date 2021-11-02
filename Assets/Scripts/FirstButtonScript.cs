using UnityEngine;
using UnityEngine.UI;
public class FirstButtonScript : MonoBehaviour
{
    void Awake()
    {
        transform.GetComponent<Button>().onClick.AddListener(GameObject.Find("Player").GetComponent<PlayerScript>().EquipClick);
    }
}
