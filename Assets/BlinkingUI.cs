using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingUI : MonoBehaviour
{
    public float blinkInterval = 0.5f; // Thời gian giữa các lần chớp nháy
    public float fadeDuration = 0.25f; // Thời gian mờ dần

    private Image imageComponent;
    private bool isBlinking = false;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        // Bắt đầu hàm Blink()
        StartBlink();
    }

    void StartBlink()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            // Gọi hàm Fade() sau mỗi blinkInterval giây
            InvokeRepeating("Fade", 0f, blinkInterval);
        }
    }

    void Fade()
    {
        // Bắt đầu coroutine để thực hiện fading
        StartCoroutine(FadeCoroutine());
    }

    IEnumerator FadeCoroutine()
    {
        Color originalColor = imageComponent.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); // Màu mờ dần đến màu trong suốt

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            // Làm mờ dần màu của Image từ màu gốc đến màu mục tiêu
            imageComponent.color = Color.Lerp(originalColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Thiết lập màu gốc lại để chớp nháy tiếp theo
        imageComponent.color = originalColor;
    }

    void OnDisable()
    {
        // Khi đối tượng bị vô hiệu hóa, hủy bỏ việc gọi hàm Fade()
        CancelInvoke("Fade");
        isBlinking = false;
    }
}
