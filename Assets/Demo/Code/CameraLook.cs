using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public Transform targetPlayer;
    public Transform cameraObject;
    [Range(0, 1)] public float moveSpeed;
    [Range(0, 1)] public float rotateSpeed;
    public float rotateStep;
    public Vector2 rotateYLimit;
    [Header("InverseSettings")]
    [Range(-1, 1)] public int invertX;
    [Range(-1, 1)] public int invertY;
    [Header("AvoidingObjects")]
    public Vector2 distanceLimit;
    [Range(0, 1)] public float adjustSpeed;
    
    private Transform mTransform;
    private Vector2 rotateXY;
    private float distance;
    
    // Start is called before the first frame update
    void Start()
    {
        mTransform = GetComponent<Transform>();
        rotateXY = Vector2.zero;
        distance = -distanceLimit.y;
    }

    // Update is called once per frame
    void Update()
    {
        //旋转
        rotateXY.x += Input.GetAxis("Mouse X")*invertX * rotateStep;
        rotateXY.y += Input.GetAxis("Mouse Y")*invertY * rotateStep;
        rotateXY.y = Mathf.Clamp(rotateXY.y, rotateYLimit.x, rotateYLimit.y);
        mTransform.rotation = Quaternion.Slerp(mTransform.rotation, 
            Quaternion.Euler(0, rotateXY.x, 0) * Quaternion.Euler(rotateXY.y, 0, 0), rotateSpeed);
    }

    private void LateUpdate()
    {
        mTransform.position = Vector3.Lerp(mTransform.position, targetPlayer.position, moveSpeed);
        //调整距离
        Vector3 nowDistance = cameraObject.position - targetPlayer.position;
        Ray ray = new Ray(targetPlayer.position, nowDistance);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, distanceLimit.y))
        {
            distance = - hit.distance;
        }
        else
        {
            distance = -distanceLimit.y;
        }

        distance = Mathf.Clamp(distance, -distanceLimit.y, -distanceLimit.x);

        cameraObject.localPosition = Vector3.Lerp(cameraObject.localPosition, new Vector3(0, 0, distance), adjustSpeed);

    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(cameraObject.position, targetPlayer.position);
    }
}
