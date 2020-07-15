using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitBase))]
public abstract class ActionComponent : MonoBehaviour
{
    protected UnitBase unit;    

    public void SetUnitBase(UnitBase _unit)
    {
        unit = _unit;
    }

    public abstract void ShowPossibilities();

    public abstract ActionComponent CopyComponentToUnit(UnitBase _unit);

    public virtual void HandleUnderAttack(UnitBase _attackingUnit)
    {

    }

    public virtual void HandleAttacking(UnitBase _attackedUnit)
    {

    }
}
