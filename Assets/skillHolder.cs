using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillHolder : MonoBehaviour
{
    public Skill skill;
    float cooldownTime;
    float activeTime;

    enum SkillState
    {
        ready,
        active,
        cooldown
    }
    SkillState state = SkillState.ready;
    public KeyCode key;


    void Update()
    {
        switch (state)
        {

        }
    }
}
