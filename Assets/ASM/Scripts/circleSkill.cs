using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Skills/CircleFireballSkill")]
public class CircleFireballSkill : Skill
{
    public GameObject circlePrefab; // Prefab của cục lửa
    public int fireballCount; // Số lượng cục lửa bay vòng quanh

    public override void Active(GameObject parent)
    {
        GameObject fireball = Instantiate(circlePrefab, parent.transform.position, Quaternion.identity);
    }
}
