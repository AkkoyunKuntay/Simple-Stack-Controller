using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("Movement Settings")]
    public float forwardSpeed;
    public float horizontalSpeed;
    private Vector3 firstMousePos;
    private Vector3 lastMousePos;
    public Vector3 swerveDelta;

    [Header("Boundary Settings")]
    public bool useBoundaries;
    public float xMin,xMax;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        CalculateTouchDelta();
        Move();



        SetBoundary();
    }

    public void Move()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        transform.Translate(swerveDelta.x * horizontalSpeed * Time.deltaTime, 0, 0);
    }
    public void CalculateTouchDelta()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstMousePos.x = Input.mousePosition.x / Screen.width;
        }

        if (Input.GetMouseButton(0))
        {
            lastMousePos.x = Input.mousePosition.x / Screen.width;
            swerveDelta = lastMousePos - firstMousePos;
            firstMousePos = lastMousePos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastMousePos = Vector3.zero;
            firstMousePos = Vector3.zero;
            swerveDelta = Vector3.zero;
            swerveDelta = lastMousePos - firstMousePos;
        }
    }

    public void SetBoundary()
    {
        if (useBoundaries)
        {
            transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, xMin, xMax),
          transform.position.y,
          transform.position.z);
        }
    }
}
