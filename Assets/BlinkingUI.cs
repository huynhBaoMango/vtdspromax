using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingUI : MonoBehaviour
{
    public float blinkInterval = 0.5f; // Thời gian giữa các lần chớp nháy
    public float fadeDuration = 0.25f; // Thời gian mờ dần
    public int blinkCount = 5; // Số lần chớp nháy

    private Graphic[] childGraphics;
    private bool isBlinking = false;

    void Start()
    {
        // Lấy tất cả các component Image và Text trong các gameobject con
        childGraphics = GetComponentsInChildren<Graphic>();
        // Bắt đầu hàm Blink()
        StartBlink();
    }

    void StartBlink()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            // Gọi hàm BlinkCoroutine để bắt đầu chớp nháy
            StartCoroutine(BlinkCoroutine());
        }
    }

    IEnumerator BlinkCoroutine()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            // Fade out tất cả cùng lúc
            yield return Fade(1f, 0f);
            // Chờ thời gian giữa các lần chớp nháy
            yield return new WaitForSeconds(blinkInterval);
            // Fade in tất cả cùng lúc
            yield return Fade(0f, 1f);
            // Chờ thời gian giữa các lần chớp nháy
            yield return new WaitForSeconds(blinkInterval);
        }

        // Đảm bảo tất cả các phần tử trở về trạng thái ban đầu
        SetAlpha(1f);
        isBlinking = false;
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        foreach (var graphic in childGraphics)
        {
            graphic.CrossFadeAlpha(endAlpha, fadeDuration, true);
        }

        yield return new WaitForSeconds(fadeDuration);
    }

    void SetAlpha(float alpha)
    {
        foreach (var graphic in childGraphics)
        {
            Color color = graphic.color;
            color.a = alpha;
            graphic.color = color;
        }
    }

    void OnDisable()
    {
        // Khi đối tượng bị vô hiệu hóa, dừng việc chớp nháy
        isBlinking = false;
        StopAllCoroutines();
        // Đảm bảo tất cả các phần tử trở về trạng thái ban đầu
        SetAlpha(1f);
    }
}
