using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GrabableComponent : MonoBehaviour
{
    Vector2 lastPosition;
    Vector2 currentSpeed;
    Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public Vector2 DEBUGspeed;

    public void MovePosition(Vector2 _newPosition)
    {
        lastPosition = rb2d.position;
        Vector2 speed = _newPosition - lastPosition;
        rb2d.velocity =speed.normalized * (speed.magnitude/0.4f);
        DEBUGspeed = rb2d.velocity;
    }
}
