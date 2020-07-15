using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathObject : MonoBehaviour
{
    SpriteRenderer rend;
    PathType type;


    public enum PathType
    {
        Move, Attack, Spawn
    }

    public void ChangePathColor(Color _c)
    {
        if(rend == null)
        {
            rend = GetComponent<SpriteRenderer>();
        }
        rend.color = _c;
    }

    public void SetPathType(PathType _type)
    {
        type = _type;
    }

    public PathType GetPathType()
    {
        return type;
    }
}
