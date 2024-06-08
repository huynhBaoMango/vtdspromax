using UnityEngine;

public class bulletInfo : MonoBehaviour
{
    public float damage;
    public string targetTag;
    void Start()
    {
        Destroy(gameObject, 1f);
    }
}
