using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : Singelton<InputManager>
{
    public bool GetKeyDown(KeyCode keyCode)
    {
        return Input.GetKeyDown(keyCode);
    }

    public bool MouseButtonDown(int index)
    {
        return Input.GetMouseButtonDown(index);                
    }

    public bool MouseButton(int index)
    {
        return Input.GetMouseButton(index);
    }

    public bool MouseButtonUp(int index)
    {
        return Input.GetMouseButtonUp(index);
    }

    public bool ShiftButton
    {
        get
        {
            return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        }
    }

    public bool ControlButton
    {
        get
        {
            return Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
        }
    }

    public Vector2 MousePosition
    {
        get
        {
            return Input.mousePosition;
        }
    }

    public float HorizontalAxisRaw
    {
        get
        {
            return Input.GetAxisRaw("Horizontal");
        }
    }
    public float VerticalAxisRaw
    {
        get
        {
            return Input.GetAxisRaw("Vertical");
        }
    }

    public float MouseScrollWheel
    {
        get
        {
            return Input.GetAxis("Mouse ScrollWheel");
        }
    }

    public bool IsPointerOverUI
    {
        get
        {
            return EventSystem.current.IsPointerOverGameObject();
        }
    }
}
