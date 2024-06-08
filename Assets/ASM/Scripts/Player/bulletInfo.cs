using UnityEngine;

public class bulletInfo : MonoBehaviour
{
    public float damage;
    void Start()
    {
        Destroy(gameObject, 1f);
    }
}
