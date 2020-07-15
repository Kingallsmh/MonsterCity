using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public static GameBoard Instance;
    public static Vector2 BoardDimensions = new Vector2();
    public static Vector2 BoardPosition = new Vector2();
    public Vector2 wantedDimensions;

    public static GameObject[,] objsOnBoard;

    private void Awake()
    {
        Instance = this;
        Init();
    }

    private void Init()
    {
        BoardDimensions = wantedDimensions;
        BoardPosition = transform.localPosition;

        objsOnBoard = new GameObject[(int)BoardDimensions.x, (int)BoardDimensions.y];
    }

    public static bool IsWithinBoard(Vector2 _pos)
    {
        Vector2 lowerBounds = new Vector2(BoardPosition.x - (BoardDimensions.x / 2), BoardPosition.y - (BoardDimensions.y / 2));
        Vector2 upperBounds = new Vector2(BoardPosition.x + (BoardDimensions.x / 2), BoardPosition.y + (BoardDimensions.y / 2));

        if (_pos.x > lowerBounds.x && _pos.x < upperBounds.x && _pos.y > lowerBounds.y && _pos.y < upperBounds.y)
        {
            return true;
        }
        return false;
    }    

    public static UnitBase IsOccupied(Vector2 _pos)
    {
        Vector2Int bdPos = GetBoardPos(_pos);
        GameObject spot = objsOnBoard[bdPos.x, bdPos.y];
        if (spot)
        {
            return spot.GetComponent<UnitBase>();            
        }
        return null;
    }

    public static bool IsOccupiedByEnemy(Vector2 _pos, PlayerHandle _playerChecking)
    {
        Vector2Int bdPos = GetBoardPos(_pos);
        GameObject spot = objsOnBoard[bdPos.x, bdPos.y];
        if (spot)
        {
            UnitBase unit = spot.GetComponent<UnitBase>();
            if (unit && unit.GetPlayer() != _playerChecking)
            {
                return true;
            }
        }
        return false;
    }

    public static void PlaceOnBoard(GameObject _obj, Vector2 _placement)
    {
        _obj.transform.SetParent(Instance.transform);
        PlaceObject(_obj, _placement);        
    }

    public static void MoveOnBoard(UnitBase _unit, Vector2 _newPos)
    {
        MoveObject(_unit.gameObject, _newPos);
    }

    public static GameObject GetObjFromBoardPosition(Vector2 _newPos)
    {
        Vector2Int bdPos = GetBoardPos(_newPos);
        return objsOnBoard[bdPos.x, bdPos.y];
    }

    public static void RemoveFromBoard(UnitBase _unit)
    {        
        Vector2Int bdPos = GetBoardPos(_unit.transform.position);
        if(objsOnBoard[bdPos.x, bdPos.y] == _unit.gameObject)
        {
            objsOnBoard[bdPos.x, bdPos.y] = null;
        }             
    }

    static void PlaceObject(GameObject _obj, Vector2 _pos)
    {        
        Vector2Int newPos = GetBoardPos(_pos);
        objsOnBoard[newPos.x, newPos.y] = _obj;
        _obj.transform.position = _pos;
    }

    static void MoveObject(GameObject _obj, Vector2 _pos)
    {
        Vector2Int bdPos = GetBoardPos(_obj.transform.position);
        Debug.Log(bdPos);
        Vector2Int newPos = GetBoardPos(_pos);
        if (objsOnBoard[bdPos.x, bdPos.y] == _obj)
        {
            objsOnBoard[bdPos.x, bdPos.y] = null;
        }
        objsOnBoard[newPos.x, newPos.y] = _obj;
        _obj.transform.position = _pos;
    }

    static Vector2Int GetBoardPos(Vector2 _pos)
    {
        return new Vector2Int((int)_pos.x , (int)_pos.y);
    }
}
