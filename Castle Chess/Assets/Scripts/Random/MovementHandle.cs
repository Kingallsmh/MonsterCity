using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandle : MonoBehaviour //Meant to help players, not shown on network
{
    public static MovementHandle Instance;
    public PathObject pathPrefab;
    public int wantedAmountOfPaths = 25;
    Pool<PathObject> pathPool;

    public Color movePathColor;
    public Color atkPathColor;
    public Color spawnPathColor;

    public enum DirectionType
    {
        Full, Linear, Cardinal
    }

    private void Awake()
    {
        Instance = this;
    }

    public void InitPathPool()
    {
        pathPool = new Pool<PathObject>();
        for (int i = 0; i < wantedAmountOfPaths; i++)
        {
            PathObject path = Instantiate(pathPrefab, transform);
            path.gameObject.SetActive(false);
            pathPool.AddToUnactive(path);
        }
    }

    public void ShowCastlePlacementPossibilities(PlayerHandle _player)
    {
        Vector2 pos = _player.botLeftPos;
        Vector2 extents = _player.dimensions;

        for (int x = 0; x < extents.x; x++)
        {
            for (int y = 0; y < extents.y; y++)
            {
                Vector2 worldPos = new Vector2(pos.x + x, pos.y + y);
                PlacePath(worldPos);
            }
        }
    }

    public void ShowPossibleAttacks(AttackComponent _atkComponent)
    {
        bool[,] array = GetPossibleMovesFromThisUnit(_atkComponent.GetAtkRange(), _atkComponent.GetAtkLinear());
        for (int x = 0; x < array.GetLength(0); x++)
        {
            for (int y = 0; y < array.GetLength(1); y++)
            {
                if (array[x, y])
                {
                    Vector2 worldPos = GetWorldPosition(_atkComponent.transform.position, x, y, _atkComponent.GetAtkRange());
                    PlacePath(worldPos, _atkComponent);
                }
            }
        }
    }

    public void ShowPossibleMoves(MovementComponent _moveComponent)
    {
        bool[,] array = GetPossibleMovesFromThisUnit(_moveComponent.movementDistance, _moveComponent.moveDirection);
        for (int x = 0; x < array.GetLength(0); x++)
        {
            for (int y = 0; y < array.GetLength(1); y++)
            {
                if (array[x, y])
                {
                    Vector2 worldPos = GetWorldPosition(_moveComponent.transform.position, x, y, _moveComponent.movementDistance);                    
                    PlacePath(worldPos, _moveComponent);
                }
            }
        }
    }

    public void ShowPossibleSpawns(CastleComponent _castle)
    {        
        bool[,] array = GetPossibleMovesFromThisUnit(_castle.spawnDistance, _castle.spawnDirection);
        for (int x = 0; x < array.GetLength(0); x++)
        {
            for (int y = 0; y < array.GetLength(1); y++)
            {
                if (array[x, y])
                {
                    Vector2 worldPos = GetWorldPosition(_castle.transform.position, x, y, _castle.spawnDistance);
                    PlacePath(worldPos, _castle);
                }
            }
        }
    }

    public void PlacePath(Vector2 _pos, MovementComponent _moveComponent)
    {
        if (GameBoard.IsWithinBoard(_pos))
        {
            Debug.Log("Move: " + _pos);
            if (!GameBoard.IsOccupied(_pos))
            {
                Debug.Log("Path: " + _pos);
                PathObject path = pathPool.GetNextObject();
                path.SetPathType(PathObject.PathType.Move);
                path.ChangePathColor(movePathColor);
                path.transform.position = _pos;
                path.gameObject.SetActive(true);
            }
        }
    }

    public void PlacePath(Vector2 _pos)
    {
        if (GameBoard.IsWithinBoard(_pos))
        {
            if (!GameBoard.IsOccupied(_pos))
            {
                PathObject path = pathPool.GetNextObject();
                path.ChangePathColor(movePathColor);
                path.transform.position = _pos;
                path.gameObject.SetActive(true);
            }
        }
    }

    public void PlacePath(Vector2 _pos, CastleComponent _castle)
    {
        if (GameBoard.IsWithinBoard(_pos))
        {
            if (!GameBoard.IsOccupied(_pos))
            {
                PathObject path = pathPool.GetNextObject();
                path.SetPathType(PathObject.PathType.Spawn);
                path.ChangePathColor(spawnPathColor);
                path.transform.position = _pos;
                path.gameObject.SetActive(true);
            }
        }
    }

    public void PlacePath(Vector2 _pos, AttackComponent _atkComponent)
    {
        if (GameBoard.IsWithinBoard(_pos))
        {
            UnitBase unit = GameBoard.IsOccupied(_pos);
            if (unit && unit.GetPlayer() != _atkComponent.GetOwner())
            {
                PathObject path = pathPool.GetNextObject();
                path.SetPathType(PathObject.PathType.Attack);
                path.ChangePathColor(atkPathColor);
                path.transform.position = _pos;
                path.gameObject.SetActive(true);
            }
        }
    }

    Vector2 GetWorldPosition(Vector2 _unitPos, int x, int y, int moveDistance)
    {
        _unitPos.x += x - moveDistance;
        _unitPos.y += y - moveDistance;
        return _unitPos;
    }

    public void RemovePathsFromBoard()
    {
        for (int i = 0; i < pathPool.GetActiveAmount(); i++)
        {
            pathPool.GetActiveObject(i).gameObject.SetActive(false);
        }
        pathPool.ClearActive();
    }

    bool[,] GetPossibleMovesFromThisUnit(int _moveDistance, DirectionType _directionType)
    {
        bool[,] moveGrid = new bool[1 + (_moveDistance * 2), 1 + (_moveDistance * 2)];
        switch (_directionType)
        {
            case DirectionType.Full:
                GetStandardMovement(ref moveGrid, _moveDistance);
                break;
            case DirectionType.Linear:
                GetLineMovement(ref moveGrid, _moveDistance);
                break;
            case DirectionType.Cardinal:
                GetCardinalMovement(ref moveGrid, _moveDistance);
                break;
        }
        //Set tile unit is on to false
        moveGrid[_moveDistance, _moveDistance] = false;
        return moveGrid;
    }

    void GetCardinalMovement(ref bool[,] _moveGrid, int _moveDistance)
    {
        int x = _moveDistance;
        int y = _moveDistance;

        for (int i = 1; i < _moveDistance + 1; i++)
        {
            _moveGrid[x + i, y] = true; //right
            _moveGrid[x - i, y] = true; //left
            _moveGrid[x, y + i] = true; //up
            _moveGrid[x, y - i] = true; //down
        }
    }

    void GetLineMovement(ref bool[,] _moveGrid, int _moveDistance)
    {
        int x = _moveDistance;
        int y = _moveDistance;

        for (int i = 1; i < _moveDistance + 1; i++)
        {
            _moveGrid[x + i, y] = true; //right
            _moveGrid[x - i, y] = true; //left
            _moveGrid[x, y + i] = true; //up
            _moveGrid[x, y - i] = true; //down
            //Diag
            _moveGrid[x + i, y + i] = true; //right up
            _moveGrid[x - i, y + i] = true; //left up
            _moveGrid[x + i, y - i] = true; //right down
            _moveGrid[x - i, y - i] = true; //left down
        }
    }

    void GetStandardMovement(ref bool[,] _moveGrid, int _moveDistance)
    {
        for (int x = 0; x < _moveGrid.GetLength(0); x++)
        {
            for (int y = 0; y < _moveGrid.GetLength(1); y++)
            {
                _moveGrid[x, y] = true;
            }
        }
    }
}
