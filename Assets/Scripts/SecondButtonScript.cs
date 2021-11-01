using UnityEngine;
using UnityEngine.UI;
public class SecondButtonScript : MonoBehaviour
{
    void Awake()
    {
        transform.GetComponent<Button>().onClick.AddListener(GameObject.Find("Player").GetComponent<PlayerScript>().OpenClick);
    }
}
