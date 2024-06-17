using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleRotate : MonoBehaviour
{
    public float speed;

    void Start()
    {
        
    }

    
    void Update()
    {
       transform.Rotate(speed * Vector3.down * Time.deltaTime);
    transform.position = new Vector3(GameObject.Find("PLAYER").transform.position.x, 2, GameObject.Find("PLAYER").transform.position.z);
    }
    
}
