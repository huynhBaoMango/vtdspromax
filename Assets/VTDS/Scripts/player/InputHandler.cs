using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Vector2 InputVector { get; private set; }
    public Vector3 MousePosition { get; private set; }
    
    public GameObject shootingEffectPrefab; // Tham chiếu đến prefab hiệu ứng bắn đạn
    
    private Animator animator;
    private Rigidbody rb;
    private bool isShooting = false;
    private GameObject shootingEffectInstance; // Instance của hiệu ứng bắn đạn

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // Đảm bảo hiệu ứng bắn đạn không hoạt động khi bắt đầu game
        if (shootingEffectPrefab != null)
        {
            shootingEffectInstance = Instantiate(shootingEffectPrefab);
            shootingEffectInstance.SetActive(false);
        }
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        InputVector = new Vector2(h, v);
        MousePosition = Input.mousePosition;

        float speed = InputVector.magnitude;

        int state = 0; 
        if (speed > 0.1f && !isShooting)
        {
            state = 1; 
        }
        else if (isShooting)
        {
            state = 2; 
        }

        // Thiết lập tham số "move" trong Animator
        animator.SetInteger("move", state);

        if (Input.GetMouseButtonDown(0)) 
        {
            StartShooting();
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            StopShooting();
        }
    }

    void StartShooting()
    {
        isShooting = true;
        animator.SetBool("isShooting", true);

        // Kích hoạt hiệu ứng bắn đạn
        if (shootingEffectInstance != null)
        {
            shootingEffectInstance.SetActive(true);
        }
    }

    void StopShooting()
    {
        isShooting = false;
        animator.SetBool("isShooting", false);

        // Tắt hiệu ứng bắn đạn
        if (shootingEffectInstance != null)
        {
            shootingEffectInstance.SetActive(false);
        }
    }
}
