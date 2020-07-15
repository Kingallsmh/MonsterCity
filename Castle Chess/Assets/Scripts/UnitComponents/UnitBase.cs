using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    SpriteRenderer rend;

    PlayerHandle ownerPlayer;
    ActionComponent[] actionCompArray;

    public void Init()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    public void ShowActionPossibilities()
    {
        for(int i = 0; i < actionCompArray.Length; i++)
        {
            actionCompArray[i].ShowPossibilities();
        }
    }

    public void RemoveActionPossibilities()
    {
        MovementHandle.Instance.RemovePathsFromBoard();
    }

    public void UnitAttacked(UnitBase _attackingUnit)
    {
        for(int i = 0; i < actionCompArray.Length; i++)
        {
            actionCompArray[i].HandleUnderAttack(_attackingUnit);
        }
    }

    public void UnitAttacking(UnitBase _attackedUnit)
    {
        for(int i = 0; i < actionCompArray.Length; i++)
        {
            actionCompArray[i].HandleAttacking(_attackedUnit);
        }
    }

    public PlayerHandle GetPlayer()
    {
        return ownerPlayer;
    }

    public void SetPlayer(PlayerHandle _player)
    {
        ownerPlayer = _player;
    }

    public Sprite GetSpriteFromRenderer()
    {
        return GetComponent<SpriteRenderer>().sprite;
    }

    public void CopyGivenUnit(UnitBase _unit)
    {
        rend.sprite = _unit.GetSpriteFromRenderer();
        AddComponentsFromUnit(_unit);
    }

    public void AddComponentsFromUnit(UnitBase _unit)
    {
        ActionComponent[] compArray = _unit.GetComponents<ActionComponent>();
        actionCompArray = new ActionComponent[compArray.Length];
        for(int i = 0; i < compArray.Length; i++)
        {
            actionCompArray[i] = compArray[i].CopyComponentToUnit(this);
            actionCompArray[i].SetUnitBase(this);
        }
    }

    public void RemoveComponents()
    {
        for(int i = 0; i < actionCompArray.Length; i++)
        {
            Destroy(actionCompArray[i]);
        }
        actionCompArray = null;
    }
}
