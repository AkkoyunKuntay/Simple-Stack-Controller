using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public Vector3 offset;
    public float smoothSpeed;
    public Transform target;

    private void LateUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        desiredPos.x = 0;
        gameObject.transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
       
        
    }
}
