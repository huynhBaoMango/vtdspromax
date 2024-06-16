using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIManager : MonoBehaviour
{
    public List<GameObject> skillIcons = new List<GameObject>(); // Danh sách biểu tượng kỹ năng
    public List<GameObject> skillDescriptionImages = new List<GameObject>(); // Danh sách hình ảnh mô tả kỹ năng
    private List<bool> firstTimePickup = new List<bool>(); // Danh sách theo dõi lần đầu nhặt kỹ năng

    void Start()
    {
        // Khởi tạo trạng thái cho tất cả các kỹ năng là sẵn sàng
        for (int i = 0; i < skillIcons.Count; i++)
        {
            skillIcons[i].SetActive(false); // Ẩn biểu tượng kỹ năng ban đầu
            skillDescriptionImages[i].SetActive(false); // Ẩn hình ảnh mô tả khi bắt đầu
            firstTimePickup.Add(true); // Đánh dấu lần đầu nhặt kỹ năng là true
        }
    }

    // Hiển thị biểu tượng của một kỹ năng
    public void ShowSkillIcon(int skillIndex)
    {
        if (skillIndex >= 0 && skillIndex < skillIcons.Count)
        {
            skillIcons[skillIndex].SetActive(true);
            SetSkillIconTransparency(skillIndex, 1.0f); // Đặt độ mờ về 1.0 (rõ ràng)
            if (firstTimePickup[skillIndex])
            {
                ShowSkillDescription(skillIndex); // Hiển thị mô tả kỹ năng lần đầu
                firstTimePickup[skillIndex] = false; // Đánh dấu đã nhặt kỹ năng
            }
        }
    }

    // Ẩn biểu tượng của một kỹ năng
    public void HideSkillIcon(int skillIndex)
    {
        if (skillIndex >= 0 && skillIndex < skillIcons.Count)
        {
            skillIcons[skillIndex].SetActive(false);
        }
    }

    // Đặt độ mờ của biểu tượng kỹ năng
    public void SetSkillIconTransparency(int skillIndex, float alpha)
    {
        if (skillIndex >= 0 && skillIndex < skillIcons.Count)
        {
            SetIconTransparency(skillIcons[skillIndex], alpha);
        }
    }

    // Đặt độ mờ của các biểu tượng trong kỹ năng
    private void SetIconTransparency(GameObject icon, float alpha)
    {
        foreach (Image image in icon.GetComponentsInChildren<Image>())
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }

    // Hiển thị hình ảnh mô tả của một kỹ năng
    public void ShowSkillDescription(int skillIndex)
    {
        if (skillIndex >= 0 && skillIndex < skillDescriptionImages.Count)
        {
            skillDescriptionImages[skillIndex].SetActive(true);
        }
        Time.timeScale=0;
    }

    // Ẩn hình ảnh mô tả của một kỹ năng
    public void HideSkillDescription(int skillIndex)
    {
        if (skillIndex >= 0 && skillIndex < skillDescriptionImages.Count)
        {
            skillDescriptionImages[skillIndex].SetActive(false);
        }
        Time.timeScale=1;
    }
}
