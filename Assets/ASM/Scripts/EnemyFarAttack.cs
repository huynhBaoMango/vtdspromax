using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFarAttack : MonoBehaviour
{
    public GameObject eggPrefab;
    public float shootingForce;
    public float detectionRange; // Phạm vi phát hiện
    public float shootCooldown = 8f; // Thời gian chờ giữa mỗi lần bắn
    private float shootTimer = 0f; // Bộ đếm thời gian

    private AnimationsController animationsController;
    private GameObject player;
    private NavMeshAgent navMeshAgent; // NavMeshAgent của kẻ địch
    private bool isShooting = false; // Biến trạng thái để kiểm tra kẻ địch có đang bắn hay không
    public Transform shootpoint;

    void Start()
    {
        player = GameObject.Find("PLAYER");
        animationsController = GetComponent<AnimationsController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        detectionRange = gameObject.GetComponent<EnemyManager>().attackRange;
        shootingForce = gameObject.GetComponent<EnemyManager>().attackRange+10;
    }

    void Update()
    {
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
        if (Vector3.Distance(transform.position, player.transform.position) < detectionRange)
       {
            DetectAndShoot();
        }
    }

    void DetectAndShoot()
    {
        if (player == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Giảm bộ đếm thời gian theo thời gian thực
        

        if (distance <= detectionRange)
        {
            navMeshAgent.isStopped = true; // Dừng kẻ địch khi bắn
            isShooting = true;
            if (shootTimer <= 0)
            {
                ShootEgg();
                shootTimer = shootCooldown; // Đặt lại bộ đếm thời gian sau khi bắn
            }
        }
        else
        {
            isShooting = false;
            navMeshAgent.isStopped = false; // Tiếp tục di chuyển kẻ địch nếu ngoài tầm
            navMeshAgent.SetDestination(player.transform.position);
        }
    }

    void ShootEgg()
    {
        Vector3 shootPoint = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
        GameObject egg = Instantiate(eggPrefab, shootPoint + transform.forward, Quaternion.identity);
        Rigidbody rb = egg.GetComponent<Rigidbody>();
        egg.GetComponent<Egg>().damage = gameObject.GetComponent<EnemyManager>().damage;
        rb.AddForce(transform.forward * shootingForce, ForceMode.Impulse);
    }
}
