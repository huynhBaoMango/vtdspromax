using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SliderRunTo1 : MonoBehaviour
{
    public bool b = true;
    public Slider slider;
    public float speed = 0.5f;
    public SliderValuePass sliderValuePass; // Tham chiếu đến script để cập nhật phần trăm

    private float time = 0f;

    void Start()
    {
        slider = GetComponent<Slider>();

        if (sliderValuePass == null)
        {
            // Tìm SliderValuePass trong cùng một GameObject hoặc tùy chỉnh để tìm đúng đối tượng
            sliderValuePass = FindObjectOfType<SliderValuePass>();
        }
    }

    void Update()
    {
        if (b)
        {
            time += Time.deltaTime * speed;
            slider.value = time;

            // Cập nhật giá trị phần trăm
            if (sliderValuePass != null)
            {
                sliderValuePass.UpdateProgress(time);
            }

            if (time >= 1)
            {
                StartCoroutine(LoadGameScene());
            }
        }
    }

    // Coroutine để chuyển cảnh
    IEnumerator LoadGameScene()
    {
        // Dừng thanh trượt
        b = false;

        // Đợi một chút để người dùng có thể thấy thanh trượt đầy 100%
        yield return new WaitForSeconds(0.5f);

        // Chuyển cảnh
        SceneManager.LoadScene("NewMap"); // Đặt tên cảnh bạn muốn tải
    }
}
