using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptObnul : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.DeleteKey("Last Range Weapon");
        PlayerPrefs.DeleteKey("Last Melee Weapon");
    }
}
