using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : MonoBehaviour
{
    public List<Skill> skills = new List<Skill>(); // Danh sách kỹ năng
    public List<bool> skillReady = new List<bool>(); // Danh sách trạng thái sẵn sàng của kỹ năng
    public List<KeyCode> keys = new List<KeyCode>(); // Danh sách phím kích hoạt kỹ năng
    private SkillUIManager skillUIManager; // Quản lý UI kỹ năng

    void Start()
    {
        skillUIManager = FindObjectOfType<SkillUIManager>(); // Tìm và gán SkillUIManager

        foreach (Skill skill in skills)
        {
            skillReady.Add(false); // Khởi tạo trạng thái không sẵn sàng
        }
    }

    void Update()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            // Sử dụng kỹ năng khi nhấn phím và kỹ năng sẵn sàng
            if (skillReady[i] && Input.GetKeyDown(keys[i]))
            {
                skills[i].Active(gameObject); // Kích hoạt kỹ năng
                skillReady[i] = false; // Đánh dấu kỹ năng không sẵn sàng
                skillUIManager.SetSkillIconTransparency(i, 0.5f); // Đặt độ trong suốt biểu tượng kỹ năng
            }
        }
    }

    // Thêm kỹ năng mới
    public void AddSkill(Skill newSkill)
    {
        skills.Add(newSkill);
        skillReady.Add(false);
    }

    // Kích hoạt kỹ năng
    public void ActivateSkill(int skillIndex)
    {
        if (skillIndex >= 0 && skillIndex < skills.Count)
        {
            skillReady[skillIndex] = true;
            skillUIManager.ShowSkillIcon(skillIndex);
            skillUIManager.SetSkillIconTransparency(skillIndex, 1f);
        }
    }
}
