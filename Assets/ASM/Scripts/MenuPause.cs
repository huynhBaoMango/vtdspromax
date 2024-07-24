using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel; // Đổi Canvas thành Panel
    public GameObject optionMenuPanel; // Thêm Panel Option
    public Button resumeButton;
    public Button restartButton;
    public Button quitButton;
    public Button optionButton; // Thêm nút Option
    public Button backButton; // Thêm nút Back trong Panel Option

    private bool isPaused = false;

    void Start()
    {
        pauseMenuPanel.SetActive(false); // Ẩn panel tạm dừng khi bắt đầu
        optionMenuPanel.SetActive(false); // Ẩn panel tùy chọn khi bắt đầu

        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
        optionButton.onClick.AddListener(ShowOptionMenu); // Thêm sự kiện cho nút Option
        backButton.onClick.AddListener(ShowPauseMenu); // Thêm sự kiện cho nút Back
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !diePanel.isDiePanelActive)
        {
            if (optionMenuPanel.activeSelf)
            {
                ShowPauseMenu(); // Quay lại menu tạm dừng nếu đang ở menu tùy chọn
            }
            else if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f; // Dừng trò chơi
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        optionMenuPanel.SetActive(false); // Ẩn panel tùy chọn khi tiếp tục trò chơi
        Time.timeScale = 1f; // Tiếp tục trò chơi
        isPaused = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Đảm bảo time scale được đặt lại
        SceneManager.LoadScene("Start"); 
    }

    public void QuitGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f; // Tiếp tục trò chơi
        isPaused = false;
    }

    public void ShowOptionMenu()
    {
        pauseMenuPanel.SetActive(false); // Ẩn panel tạm dừng
        optionMenuPanel.SetActive(true); // Hiển thị panel tùy chọn
    }

    public void ShowPauseMenu()
    {
        optionMenuPanel.SetActive(false); // Ẩn panel tùy chọn
        pauseMenuPanel.SetActive(true); // Hiển thị lại panel tạm dừng
    }
}
