using UnityEngine;
using UnityEngine.UI;
public class ScriptObnul : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.DeleteKey("Last Range Weapon");
        PlayerPrefs.DeleteKey("Last Melee Weapon");

        if (GameObject.Find("ServiceObjects(dd)") == null)
        {
            DontDestroyOnLoad(GameObject.Find("ServiceObjects"));
            GameObject.Find("ServiceObjects").name = "ServiceObjects(dd)";
        }
        else
        {
            Destroy(GameObject.Find("ServiceObjects(dd)"));
            DontDestroyOnLoad(GameObject.Find("ServiceObjects"));
            GameObject.Find("ServiceObjects").name = "ServiceObjects(dd)";
        }
        
    }
}
