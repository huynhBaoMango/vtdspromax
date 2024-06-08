using System;
using UnityEngine;

public class TopdownCharacter : MonoBehaviour
{
    private InputHandler _input;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private bool rotateTowardsMouse;
    
    
    
    void Start()
    {
        _input = GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);
        var movementVector = MoveTowardsTarget(targetVector);
        if (!rotateTowardsMouse)
        {
            RotateTowardMovementVector(movementVector);
        }
        else
        {
            RotateTowardsMouseVector();
        }
        camera.transform.position= new Vector3(transform.position.x, transform.position.y + 8, transform.position.z-4);
    }

    private void RotateTowardMovementVector(Vector3 movementVector)
    {
        if (movementVector.magnitude == 0) return;
        var rotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);
    }

    private void RotateTowardsMouseVector()
    {
        Ray ray = camera.ScreenPointToRay(_input.MousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;  // Giữ nguyên chiều cao của nhân vật
            transform.LookAt(target);
        }
    }

    private Vector3 MoveTowardsTarget(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime;
        targetVector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;  // Cập nhật vị trí đúng cách
        return targetVector;
    }
}
