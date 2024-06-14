using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleRotate : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speed * Vector3.down * Time.deltaTime);
        transform.position = GameObject.Find("PLAYER").transform.position;
    }
}
