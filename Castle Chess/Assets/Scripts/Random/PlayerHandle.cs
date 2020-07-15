using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandle : MonoBehaviour
{
    public Camera cam;
    public UnitBase unitPrefab;
    public UnitBase soldierPrefab, archerPrefab, cavalryPrefab, castlePrefab;

    bool isTurn = false;
    UnitBase currentHighlighted;
    Pool<UnitBase> unitPool;
    List<UnitBase> playerUnits;

    List<UnitBase> castleList = new List<UnitBase>();
    [SerializeField]
    UnitType currentWantedUnit = UnitType.Soldier;

    //Caslte placing coordinates
    public Vector2 botLeftPos;
    public Vector2 dimensions;

    public enum UnitType
    {
        Castle, Soldier, Archer, Cavalry
    }

    // Start is called before the first frame update
    void Start()
    {
        playerUnits = new List<UnitBase>();
        unitPool = new Pool<UnitBase>();
        for(int i = 0; i < 18; i++)
        {
            SpawnNewUnit(unitPrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurn)
        {
            ClickHandle();
        }        
    }

    public void AddCastleToPlayer(UnitBase _castle)
    {
        castleList.Add(_castle);
    }

    public int GetCastleAmount()
    {
        return castleList.Count;
    }

    public void SetIsTurn(bool _isTurn)
    {
        isTurn = _isTurn;
    }

    void SpawnNewUnit(UnitBase _prefab)
    {
        UnitBase unit = Instantiate(_prefab, transform);
        unit.Init();
        unit.SetPlayer(this);
        unitPool.AddToUnactive(unit);
        unit.gameObject.SetActive(false);        
    }

    public void NewUnitOnBoard(Vector2 _pos, UnitType _type)
    {
        UnitBase unit = unitPool.GetNextObject();
        unit.gameObject.SetActive(true);
        SetUnitType(unit, _type);
        playerUnits.Add(unit);
        GameBoard.PlaceOnBoard(unit.gameObject, _pos);
    }   
    
    void SetUnitType(UnitBase _unit, UnitType _type)
    {
        switch (_type)
        {
            case UnitType.Soldier:
                _unit.CopyGivenUnit(soldierPrefab);
                break;
            case UnitType.Archer:
                _unit.CopyGivenUnit(archerPrefab);
                break;
            case UnitType.Cavalry:
                _unit.CopyGivenUnit(cavalryPrefab);
                break;
            case UnitType.Castle:
                _unit.CopyGivenUnit(castlePrefab);
                AddCastleToPlayer(_unit);
                break;
        }        
    }

    public void RemoveUnit(UnitBase _unit)
    {
        _unit.gameObject.SetActive(false);
        _unit.transform.SetParent(transform);
        playerUnits.Remove(_unit);
        _unit.RemoveComponents();
        unitPool.ReturnObject(_unit);
    }

    public void SelectUnit(UnitBase _newUnit)
    {        
        currentHighlighted = _newUnit;
        currentHighlighted.ShowActionPossibilities();
    }

    public void UnselectUnit()
    {
        if (currentHighlighted)
        {
            currentHighlighted.RemoveActionPossibilities();
            currentHighlighted = null;
        }
    }

    public UnitBase GetSelectedUnit()
    {
        return currentHighlighted;
    }

    public void ClickHandle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider)
            {
                //Unit Select
                UnitBase unit = hit.collider.GetComponent<UnitBase>();
                if (unit && unit.GetPlayer() == this)
                {
                    UnselectUnit();
                    SelectUnit(unit);
                    return;
                }

                //Path select
                PathObject path = hit.collider.GetComponent<PathObject>();
                if(path)
                {
                    GamePlayHandle.Instance.HandleClickingPath(this, path);
                    UnselectUnit();
                    return;
                }
            }
            UnselectUnit();
        }
    }

    public UnitType GetCurrentWantedUnitType()
    {
        return currentWantedUnit;
    }

    public void SetCurrentWantedUnitType(UnitType _type)
    {
        currentWantedUnit = _type;
    }

    public List<UnitBase> GetUnitList()
    {
        return playerUnits;
    }
}
