using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private EnemyManager emanager;
     public float damage;
    

    void Start()
    {
        
        emanager = GetComponent<EnemyManager>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("bullet")) // Chỉ xử lý khi đối tượng va chạm là viên đạn
        {
            bulletInfo bullet = other.gameObject.GetComponent<bulletInfo>();
            if (bullet != null && bullet.targetTag == "Enemy") // Đảm bảo rằng đối tượng đích của viên đạn là Enemy
            {
                float damageTaken = bullet.damage;
                emanager.TakeDamage(damageTaken); // Trừ máu Enemy
                Destroy(other.gameObject); // Hủy viên đạn sau khi gây sát thương
            }
        }
    }
}
