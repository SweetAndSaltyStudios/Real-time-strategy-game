using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEngine : MonoBehaviour
{
    [Header("Camera Movement Variables")]
    public bool MoveWithMouse = false;
    public float PanSpeed = 10f;
    public float PanBorderThickness = 10f;
    public float ScrollSpeed = 200f;
    public Vector2 ScrollLimit = new Vector2(5, 20);
    public Vector2 PanLimit = new Vector2(10, 10);

    private Vector2 screenSize = new Vector2(Screen.width, Screen.height);
    private Vector3 currentPosition;
    private Vector2 currentMovementInputAxis;

    public bool CursorOnRight
    {
        get
        {
            if(MoveWithMouse == false)
            {
                return false;
            }

            return InputManager.Instance.MousePosition.x >= screenSize.x - PanBorderThickness;
        }
    }

    public bool CursorOnLeft
    {
        get
        {
            if (MoveWithMouse == false)
            {
                return false;
            }
            return InputManager.Instance.MousePosition.x <= PanBorderThickness;
        }
    }

    public bool CursorOnUp
    {
        get
        {
            if (MoveWithMouse == false)
            {
                return false;
            }
            return InputManager.Instance.MousePosition.y >= screenSize.y - PanBorderThickness;
        }
    }

    public bool CursorOnDown
    {
        get
        {
            if (MoveWithMouse == false)
            {
                return false;
            }
            return InputManager.Instance.MousePosition.y <= PanBorderThickness;
        }
    }

    private void Update()
    {
        GetInputs();

        HorizontalMovement(); 
        VerticalMovement();

        MoveCamera();
    }

    private void GetInputs()
    {
        currentPosition = transform.position;

        currentMovementInputAxis = new Vector2(
            InputManager.Instance.HorizontalAxisRaw,
            InputManager.Instance.VerticalAxisRaw);
    }

    private void HorizontalMovement()
    {
        if (currentMovementInputAxis.x > 0 || CursorOnRight)
        {
            currentPosition.x += PanSpeed * Time.deltaTime;
        }
        if (currentMovementInputAxis.x < 0 || CursorOnLeft)
        {
            currentPosition.x -= PanSpeed * Time.deltaTime;
        }
    }

    private void VerticalMovement()
    {
        if (currentMovementInputAxis.y > 0 || CursorOnUp)
        {
            currentPosition.z += PanSpeed * Time.deltaTime;
        }
        if (currentMovementInputAxis.y < 0 || CursorOnDown)
        {
            currentPosition.z -= PanSpeed * Time.deltaTime;
        }
    }

    private void MoveCamera()
    {
        currentPosition.y -= InputManager.Instance.MouseScrollWheel * ScrollSpeed * Time.deltaTime;
        currentPosition.x = Mathf.Clamp(currentPosition.x, -PanLimit.x, PanLimit.x);
        currentPosition.y = Mathf.Clamp(currentPosition.y, ScrollLimit.x, ScrollLimit.y);
        currentPosition.z = Mathf.Clamp(currentPosition.z, -PanLimit.y, PanLimit.y);
        transform.position = currentPosition;
    }
}
