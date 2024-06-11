using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public void Restart()
    {
        // Đặt lại thời gian trò chơi về trạng thái bình thường
        Time.timeScale = 1;

        // Lấy tên của cảnh hiện tại
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Tải lại cảnh hiện tại
        SceneManager.LoadScene(currentSceneName);

        // Thêm logic để bắt đầu lại trò chơi
        Debug.Log("Bắt đầu lại");
    }

    public void Option()
    {
        // Load the new game scene
        Debug.Log("Mở Option");
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
