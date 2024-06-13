using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFarAttack : MonoBehaviour
{
    public GameObject eggPrefab;
    public float shootingForce = 10f;
    public float detectionRange = 10f; // Phạm vi phát hiện
    private AnimationsController animationsController;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PLAYER");
        animationsController = GetComponent<AnimationsController>();
    }

    void Update()
    {
        DetectAndShoot();
    }

    void DetectAndShoot()
    {
        if (player == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= detectionRange)
        {
            ShootEgg();
        }
    }

    void ShootEgg()
    {
        animationsController.Attack();
        GameObject egg = Instantiate(eggPrefab, transform.position + transform.forward, Quaternion.identity);
        Rigidbody rb = egg.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * shootingForce, ForceMode.Impulse);
    }
}
