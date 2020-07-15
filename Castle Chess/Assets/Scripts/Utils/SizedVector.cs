using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SizedVector
{
    public float[] valueArray;

    public SizedVector(float f)
    {
        valueArray = new float[1];
        valueArray[0] = f;
    }

    public SizedVector(float[] _array)
    {
        valueArray = new float[_array.Length];
        for(int i = 0; i < _array.Length; i++)
        {
            valueArray[i] = _array[i];
        }
    }

    public SizedVector(Vector2 _v2)
    {
        valueArray = new float[2];
        valueArray[0] = _v2.x;
        valueArray[1] = _v2.y;        
    }

    public SizedVector(Vector3 _v3)
    {
        valueArray = new float[3];
        valueArray[0] = _v3.x;
        valueArray[1] = _v3.y;
        valueArray[2] = _v3.z;
    }

    public SizedVector(Color _c)
    {
        valueArray = new float[4];        
        valueArray[0] = _c.r;
        valueArray[1] = _c.g;
        valueArray[2] = _c.b;
        valueArray[3] = _c.a;
    }

    public SizedVector(Quaternion _q)
    {
        valueArray = new float[4];
        valueArray[0] = _q.x;
        valueArray[1] = _q.y;
        valueArray[2] = _q.z;
        valueArray[3] = _q.w;
    }

    public SizedVector ReturnValueCopy()
    {
        return new SizedVector(valueArray);        
    }

    public override bool Equals(object obj)
    {
        //Check for null and compare run-time types.
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        return isEqual((SizedVector)obj);
    }

    public bool isEqual(SizedVector _vector)
    {
        if(valueArray == null)
        {
            if (_vector.valueArray != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        if(valueArray.Length != _vector.valueArray.Length)
        {
            return false;
        }
        for(int i = 0; i < valueArray.Length; i++)
        {
            if(valueArray[i] != _vector.valueArray[i])
            {
                return false;
            }
        }
        return true;
    }

    public Vector2 ReturnVector2()
    {
        return new Vector2(valueArray[0], valueArray[1]);
    }

    public Vector3 ReturnVector3()
    {
        return new Vector3(valueArray[0], valueArray[1], valueArray[2]);
    }

    public Quaternion ReturnQuaternion()
    {
        return new Quaternion(valueArray[0], valueArray[1], valueArray[2], valueArray[3]);
    }

    public Color ReturnColor()
    {
        return new Color(valueArray[0], valueArray[1], valueArray[2], valueArray[3]);
    }

    public int GetLength()
    {
        return valueArray.Length;
    }

    public override string ToString()
    {
        return Print();
    }

    public string Print()
    {
        string s = "(";
        for(int i = 0; i < valueArray.Length; i++)
        {
            s += " " + valueArray[i];
        }
        s += " )";
        return s;
    }
}
