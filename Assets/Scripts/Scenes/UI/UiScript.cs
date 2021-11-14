using UnityEngine;
using UnityEngine.SceneManagement;
public class UiScript : MonoBehaviour
{
    bool isPaused = false;
    public void Pause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            transform.GetChild(5).gameObject.SetActive(false);
        }
        else if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            transform.GetChild(5).gameObject.SetActive(true);
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
