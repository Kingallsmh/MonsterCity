using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleComponent : ActionComponent
{
    public bool isObjective = true;
    public int spawnDistance;
    public MovementHandle.DirectionType spawnDirection = MovementHandle.DirectionType.Full;

    public PlayerHandle GetOwner()
    {
        return unit.GetPlayer();
    }

    public override void ShowPossibilities()
    {
        if(unit.GetPlayer().GetUnitList().Count != GamePlayHandle.UNIT_MAX)
        {
            MovementHandle.Instance.ShowPossibleSpawns(this);
        }        
    }

    public override ActionComponent CopyComponentToUnit(UnitBase _unit)
    {
        CastleComponent unitCastle = _unit.GetComponent<CastleComponent>();
        if (!unitCastle)
        {
            unitCastle =_unit.gameObject.AddComponent<CastleComponent>();
        }
        unitCastle.isObjective = isObjective;
        unitCastle.spawnDistance = spawnDistance;
        unitCastle.spawnDirection = spawnDirection;
        return unitCastle;
    }

    public override void HandleUnderAttack(UnitBase _attackingUnit)
    {
        if (isObjective)
        {
            _attackingUnit.GetPlayer().NewUnitOnBoard(transform.position, PlayerHandle.UnitType.Castle);
            _attackingUnit.GetPlayer().RemoveUnit(_attackingUnit);
        }
    }
}
