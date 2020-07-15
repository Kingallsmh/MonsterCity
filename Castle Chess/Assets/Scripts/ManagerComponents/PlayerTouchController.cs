using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerInput))]
public class PlayerTouchController : MonoBehaviour
{
    Vector2 lastTouchPoint, currentPoint;
    PlayerInput input;
    GrabableComponent currentGrabbed;

    [Header("Required Properties")]
    public Camera cam;
    ////public Satellite launchObject;
    //public LineRenderer launchRender;
    //public float launchSpeedMultiplier = 1;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        ClickScreen();       
    }

    public Vector2 GetScreenTouchPoint()
    {
        return cam.ScreenToWorldPoint(input.GetClickPosition());
    }

    /// <summary>
    /// Intended as an Update-able function, check the mouse button(0) input status and 
    /// </summary>
    public void ClickScreen()
    {
        if (input.GetPlayerClickDown())
        {
            //LaunchSatelliteStart();
            FindGrabableObject(GetScreenTouchPoint());
        }        
        if (input.GetPlayerClick())
        {
            //LaunchSatelliteUpdate();
            DragGrabable(GetScreenTouchPoint());


        }
        else if (input.GetPlayerClickUp())
        {
            //LaunchSatelliteEnd();
            DropGrabable();
        }
    }

    void FindGrabableObject(Vector2 _worldPoint)
    {
        RaycastHit2D hit = Physics2D.Raycast(_worldPoint, Vector2.zero);
        if (hit)
        {
            GrabableComponent g = hit.transform.GetComponent<GrabableComponent>();
            if (g)
            {
                currentGrabbed = g;
            }
        }
    }

    void DragGrabable(Vector2 _worldPoint)
    {
        if (currentGrabbed)
        {
            currentGrabbed.MovePosition(_worldPoint);
        }
    }

    void DropGrabable()
    {
        if (currentGrabbed)
        {
            currentGrabbed = null;
        }
    }

    ///// <summary>
    ///// Create the location of where the satellite will spawn and the first point of the lineRenderer.
    ///// </summary>
    //void LaunchSatelliteStart()
    //{
    //    lastTouchPoint = GetScreenTouchPoint();        
    //    launchRender.positionCount = 2;
    //    launchRender.enabled = true;
    //    //launchRender.transform.position = lastTouchPoint;
    //    launchRender.SetPosition(0, lastTouchPoint);
    //    Debug.Log(lastTouchPoint);
    //}

    ///// <summary>
    ///// Update the end position of the line and the current point.
    ///// </summary>
    //void LaunchSatelliteUpdate()
    //{
    //    currentPoint = GetScreenTouchPoint();
    //    launchRender.SetPosition(1, currentPoint - (Vector2)launchRender.transform.position);
    //}

    /// <summary>
    /// Create the Satellite and give it a velocity and direction based on the last and current point vectors.
    /// Reset the LineRender.
    /// </summary>
    //void LaunchSatelliteEnd()
    //{
    //    Satellite sat = GenerateSatelitte(lastTouchPoint);
    //    GiveSatelliteWantedVelocity(sat);
    //    launchRender.enabled = false;
    //    launchRender.positionCount = 0;
    //}

    ///// <summary>
    ///// Create a Satellite at the given point in scene.
    ///// </summary>
    ///// <param name="_launchStartPos"></param>
    ///// <returns></returns>
    //Satellite GenerateSatelitte(Vector2 _launchStartPos)
    //{
    //    Satellite newSatelitte = Instantiate(launchObject);
    //    newSatelitte.Init();
    //    newSatelitte.transform.position = _launchStartPos;
    //    return newSatelitte;
    //}

    ///// <summary>
    ///// Calculate the launch velocity and direction from the last and current points.
    ///// Intended to be used in the LaunchSatelliteEnd().
    ///// </summary>
    ///// <param name="_launchSatelitte"></param>
    //void GiveSatelliteWantedVelocity(Satellite _launchSatelitte)
    //{
    //    Vector2 launchVector = lastTouchPoint - currentPoint;
    //    float speed = launchVector.magnitude * launchSpeedMultiplier;
    //    _launchSatelitte.LaunchSatelitte(launchVector, speed);
    //}
}
