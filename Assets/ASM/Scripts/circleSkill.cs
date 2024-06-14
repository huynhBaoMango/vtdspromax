using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Skills/CircleFireballSkill")]
public class CircleFireballSkill : Skill
{
    public GameObject circlePrefab; // Prefab của cục lửa

    public override void Active(GameObject parent)
    {
        GameObject fireballs = Instantiate(circlePrefab, parent.transform.position, Quaternion.identity);
        Destroy(fireballs, activeTime);
        
    }
}
