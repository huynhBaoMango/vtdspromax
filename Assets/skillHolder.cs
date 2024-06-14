using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHolder : MonoBehaviour
{
    public List<Skill> skills = new List<Skill>(); // Danh sách các kỹ năng
    List<float> cooldownTimes = new List<float>(); // Danh sách thời gian cooldown của từng kỹ năng
    List<float> activeTimes = new List<float>(); // Danh sách thời gian hoạt động của từng kỹ năng
    List<SkillState> states = new List<SkillState>(); // Danh sách trạng thái của từng kỹ năng
    public List<KeyCode> keys = new List<KeyCode>(); // Danh sách keycodes tương ứng với từng kỹ năng

    enum SkillState
    {
        Ready,
        Active,
    }

    void Start()
    {
        // Khởi tạo danh sách thời gian cooldown và active ban đầu
        foreach (Skill skill in skills)
        {
            cooldownTimes.Add(0f);
            activeTimes.Add(0f);
            states.Add(SkillState.Ready);
        }
    }

    void Update()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            switch (states[i])
            {
                case SkillState.Ready:
                    if (Input.GetKeyDown(keys[i]) && cooldownTimes[i] <= 0)
                    {
                        skills[i].Active(gameObject);
                        states[i] = SkillState.Active;
                        activeTimes[i] = skills[i].activeTime;
                    }
                    break;
                case SkillState.Active:
                    if (activeTimes[i] > 0)
                    {
                        activeTimes[i] -= Time.deltaTime;
                    }
                    else
                    {
                        skills.RemoveAt(i);
                        activeTimes.RemoveAt(i);
                        states.RemoveAt(i);
                        keys.RemoveAt(i);
                    }
                    break;
            }
        }
    }
}
