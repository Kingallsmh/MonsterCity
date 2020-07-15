using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackComponent : ActionComponent
{
    public int atkRange = 0;
    public MovementHandle.DirectionType atkType = MovementHandle.DirectionType.Full;
    public bool atkRangeSameAsMovement = true;
    public bool atkLinearSameAsMovement = true;

    public PlayerHandle GetOwner()
    {
        return unit.GetPlayer();
    }

    public override void ShowPossibilities()
    {
        MovementHandle.Instance.ShowPossibleAttacks(this);
    }

    public int GetAtkRange()
    {
        if (atkRangeSameAsMovement)
        {
            return GetComponent<MovementComponent>().movementDistance;
        }
        else
        {
            return atkRange;
        }
    }

    public MovementHandle.DirectionType GetAtkLinear()
    {
        if (atkLinearSameAsMovement)
        {
            return GetComponent<MovementComponent>().moveDirection;
        }
        else
        {
            return atkType;
        }
    }

    public override ActionComponent CopyComponentToUnit(UnitBase _unit)
    {
        AttackComponent unitAtk = _unit.GetComponent<AttackComponent>();
        if (!unitAtk)
        {
            unitAtk = _unit.gameObject.AddComponent<AttackComponent>();
        }
        unitAtk.atkRange = atkRange;
        unitAtk.atkType = atkType;
        unitAtk.atkRangeSameAsMovement = atkRangeSameAsMovement;
        unitAtk.atkLinearSameAsMovement = atkLinearSameAsMovement;
        return unitAtk;
    }

    public override void HandleAttacking(UnitBase _attackedUnit)
    {
        _attackedUnit.UnitAttacked(unit);
        if (atkRangeSameAsMovement)
        {
            GameBoard.MoveOnBoard(unit, _attackedUnit.transform.position);
        }        
        _attackedUnit.GetPlayer().RemoveUnit(_attackedUnit);
    }
}
