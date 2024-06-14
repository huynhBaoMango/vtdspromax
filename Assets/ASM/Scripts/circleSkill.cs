using UnityEngine;

[CreateAssetMenu(menuName = "Skills/CircleFireballSkill")]
public class CircleFireballSkill : Skill
{
    public GameObject circlePrefab; // Prefab của cục lửa

    public override void Active(GameObject parent)
    {
        GameObject fireballs = Instantiate(circlePrefab, new Vector3(parent.transform.position.x, 2, parent.transform.position.z), Quaternion.identity);
        Destroy(fireballs, activeTime);
        
    }
}
