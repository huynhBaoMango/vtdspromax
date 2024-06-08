using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIngame : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = GameObject.Find("PLAYER").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + 18, player.position.z - 10);
    }
}