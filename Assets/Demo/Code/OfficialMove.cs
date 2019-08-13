using UnityEngine;
using System.Collections;

public class OfficialMove : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float AirFriction = 20.0F;
    public Transform cameraTransform;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (controller.isGrounded)
        {
            
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            //这里执行坐标转换
            Vector3 forward = transform.position - cameraTransform.position;
            forward.y = 0;
            forward.Normalize();
            Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;
            moveDirection = forward * v + right * h;

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        moveDirection.y = Mathf.Max(moveDirection.y, -AirFriction * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);
    }

}
