using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("NewMap");
    }
        
    public void QuitGame()
    {
        Application.Quit();
    }
}
