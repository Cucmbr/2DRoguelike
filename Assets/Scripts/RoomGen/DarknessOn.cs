using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DarknessOn : MonoBehaviour
{
    public bool dark = false;
    public bool nothing = false;
    public GameObject Darkness;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && dark == false)
        {
            Darkness.gameObject.SetActive(true);
            dark = true;
            nothing = false;
            Debug.Log("Click");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dark = false;
            nothing = true;
        }
    }
    private async void Update () 
    {
        if(nothing == true)
        {
            await Task.Delay(500);
            Darkness.gameObject.SetActive(false);
        }
        
    }
}
