using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Vector2 InputVector { get; private set; }
    public Vector3 MousePosition { get; private set; }
    
    Animator animator;
    Rigidbody rb;
    
    bool isShooting = false;

    public GameObject muzzleFlash; // Tham chiếu đến GameObject của hiệu ứng nổ đầu súng

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        // Đảm bảo hiệu ứng nổ đầu súng tắt khi bắt đầu game
        if (muzzleFlash)
        {
            muzzleFlash.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Muzzle flash GameObject is not assigned.");
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

        // Thiết lập tham số move trong Animator
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
        
        // Bật hiệu ứng nổ đầu súng
        if (muzzleFlash)
        {
            Debug.Log("Muzzle flash activated.");
            muzzleFlash.SetActive(true);
        }
    }

    void StopShooting()
    {
        isShooting = false;
        animator.SetBool("isShooting", false);

        // Tắt hiệu ứng nổ đầu súng
        if (muzzleFlash)
        {
            Debug.Log("Muzzle flash deactivated.");
            muzzleFlash.SetActive(false);
        }
    }
}
