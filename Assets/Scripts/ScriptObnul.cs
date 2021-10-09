using UnityEngine;

public class ScriptObnul : MonoBehaviour
{
    private void Awake()
    {
        //ќбнуление временно-используемых переменных
        PlayerPrefs.DeleteKey("Last Range Weapon");
        PlayerPrefs.DeleteKey("Last Melee Weapon");
    }
}
