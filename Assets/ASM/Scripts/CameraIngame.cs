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
        transform.position = new Vector3(player.position.x, player.position.y + 12, player.position.z - 6);
        FindAnyObjectByType<AudioManager>().PlayButWait("theme1");
    }
}
