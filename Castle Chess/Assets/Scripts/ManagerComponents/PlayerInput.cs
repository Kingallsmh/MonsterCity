using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    bool mouseDownFrame = false;
    bool mouseStillDown = false;
    bool mouseUp = false;
    Vector3 mouseDownSpot;
    
    // Update is called once per frame
    void Update()
    {
        HandleClickOnScreen();
    }

    void HandleClickOnScreen()
    {
        HandleMouseStates();
        mouseDownSpot = GetMouseInteractionPoint();        
    }

    void HandleMouseStates()
    {
        bool mousePress = CheckForMouseInteract();
        if (mousePress)
        {
            if (!mouseStillDown)
            {
                mouseDownFrame = true;
            }
            else
            {
                mouseDownFrame = false;
            }
        }
        else
        {
            if (mouseStillDown)
            {
                mouseUp = true;
            }
            else
            {
                mouseUp = false;
            }
        }
        mouseStillDown = mousePress;
    }

    bool CheckForMouseInteract()
    {
        if (Input.touchCount > 0)
        {
            return true;
        }
        else if (Input.GetMouseButton(0))
        {
            return true;
        }
        return false;
    }

    Vector3 GetMouseInteractionPoint()
    {
        Vector3 point = Vector3.zero;
        if (Input.touchCount > 0)
        {
            //print("Touch up " + Input.touches[0].position);
            point = Input.touches[0].position;
        }
        else if (Input.GetMouseButton(0))
        {
            //print("Mouse up " + Input.mousePosition);
            point = Input.mousePosition;
        }
        return point;
    }

    public bool GetPlayerClickDown()
    {
        return mouseDownFrame;
    }

    public bool GetPlayerClick()
    {
        return mouseStillDown;
    }

    public bool GetPlayerClickUp()
    {
        return mouseUp;
    }

    public Vector3 GetClickPosition()
    {
        return mouseDownSpot;
    }
}
