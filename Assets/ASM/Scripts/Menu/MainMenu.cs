using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("loading");
    }
        
    public void QuitGame()
    {
        Application.Quit();
    }
}
