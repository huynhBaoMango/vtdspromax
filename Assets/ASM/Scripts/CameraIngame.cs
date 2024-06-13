using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIngame : MonoBehaviour
{
    private Transform player;
    public int yOffset, zOffset;

    // Thêm các biến cho rotation
    public float rotationX;
    public float rotationY;
    public float rotationZ;

    void Start()
    {
        player = GameObject.Find("PLAYER").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Cập nhật vị trí của camera
        transform.position = new Vector3(player.position.x, player.position.y + yOffset, player.position.z - zOffset);

        // Cập nhật rotation của camera
        transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
    }
}
