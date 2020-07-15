using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementComponent : ActionComponent
{
    public int movementDistance = 1;
    public MovementHandle.DirectionType moveDirection = MovementHandle.DirectionType.Full;

    public PlayerHandle GetOwner()
    {
        return unit.GetPlayer();
    }

    public override void ShowPossibilities()
    {
        MovementHandle.Instance.ShowPossibleMoves(this);
    }

    public override ActionComponent CopyComponentToUnit(UnitBase _unit)
    {
        MovementComponent unitMove = _unit.GetComponent<MovementComponent>();
        if (!unitMove)
        {
            unitMove = _unit.gameObject.AddComponent<MovementComponent>();
        }
        unitMove.movementDistance = movementDistance;
        unitMove.moveDirection = moveDirection;
        return unitMove;
    }
}
