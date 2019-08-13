using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCharacter : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform camera;
    public Transform virtualCapsule;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        SuXitongMove();
    }

    private void SuXitongMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //这里执行坐标转换
        Vector3 forward = transform.position - camera.position;
        forward.y = 0;
        forward.Normalize();
        Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;

        //transform.Translate((h * right + v * forward) * moveSpeed * Time.deltaTime);
        controller.Move((h * right + v * forward) * moveSpeed);
    }

    private void WuWenjieMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            Vector3 forwardDirection = new Vector3(h, 0, v);
            float z = virtualCapsule.transform.rotation.eulerAngles.z;
            float x = virtualCapsule.transform.rotation.eulerAngles.x;
            forwardDirection = Quaternion.Euler(0, z, 0) * forwardDirection;
            Vector3 rightDirection = Quaternion.Euler(x, 0, 0) * forwardDirection;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ||
                Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                transform.Translate(forwardDirection * 1 * moveSpeed * Time.deltaTime, Space.Self);

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ||
                Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                transform.Translate(rightDirection * 1 * moveSpeed * Time.deltaTime, Space.Self);

        }

    }
}

