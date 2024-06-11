using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimap : MonoBehaviour
{
    public Transform player;
    void Start()
    {
        
    }
    private void LateUpdate()
    {
        Vector3 newPos = player.position;
        newPos.y= transform.position.y;
        transform.position = newPos;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
