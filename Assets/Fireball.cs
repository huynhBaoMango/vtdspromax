using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Transform center; // Trung tâm vòng tròn (thường là người chơi)
    public float radius = 2.0f; // Bán kính của vòng tròn
    public float speed = 2.0f; // Tốc độ quay quanh vòng tròn

    private float angle; // Góc quay hiện tại

    void Start()
    {
        if (center == null)
        {
            center = transform.parent;
        }

        transform.position = (transform.position - center.position).normalized * radius + center.position;
        angle = Random.Range(0, 360); // Để cục lửa bắt đầu từ một góc ngẫu nhiên
    }

    void Update()
    {
        // Di chuyển xung quanh người chơi theo hình tròn
        angle += speed * Time.deltaTime;
        Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
        transform.position = center.position + offset;
    }
}
