using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayHandle : MonoBehaviour
{
    public static GamePlayHandle Instance;
    public List<PlayerHandle> playerList;

    public int maxUnits = 10;
    public static int UNIT_MAX;
    public int maxCastles = 2;
    public static int CASTLE_MAX;

    int currentPlayerTurn;
    Phase currentPhase = Phase.CastlePlacement;

    public enum Phase
    {
        CastlePlacement, UnitMove
    }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //TODO add players dynamically

        UNIT_MAX = maxUnits;
        CASTLE_MAX = maxCastles;

        MovementHandle.Instance.InitPathPool();
        currentPhase = Phase.CastlePlacement;
        currentPlayerTurn = 0; //Or make this random
        playerList[currentPlayerTurn].SetIsTurn(true);
        GameFlow();
    }

    void GameFlow()
    {
        switch (currentPhase)
        {
            case Phase.CastlePlacement:
                CastlePlacementFlow();
                break;
            case Phase.UnitMove:
                UnitMoveFlow();
                break;
        }
    }

    void CastlePlacementFlow()
    {
        bool allCastlesPlaced = true;
        for (int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i].GetCastleAmount() != maxCastles)
            {
                allCastlesPlaced = false;
            }
        }
        if (allCastlesPlaced)
        {
            currentPhase = Phase.UnitMove;
            MovementHandle.Instance.RemovePathsFromBoard();
        }
        else
        {
            MovementHandle.Instance.ShowCastlePlacementPossibilities(playerList[currentPlayerTurn]);
        }
    }

    void UnitMoveFlow()
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i].GetCastleAmount() == 0)
            {
                playerList.RemoveAt(i);
            }
        }
        if(playerList.Count == 1)
        {
            Debug.Log("We have a winner!");
        }
    }

    public void AddPlayerToGame(PlayerHandle _player)
    {
        playerList.Add(_player);
    }

    public void EndTurnForPlayer(PlayerHandle _player)
    {
        MovementHandle.Instance.RemovePathsFromBoard();
        if (playerList[currentPlayerTurn] != _player)
        {
            Debug.LogError("Should not be this player's turn: " + _player);
            return;
        }
        _player.SetIsTurn(false);
        SetNextPlayerTurn();
        GameFlow();
    }

    void SetNextPlayerTurn()
    {
        if(currentPlayerTurn >= playerList.Count - 1)
        {
            currentPlayerTurn = 0;
        }
        else
        {
            currentPlayerTurn++;
        }
        playerList[currentPlayerTurn].SetIsTurn(true);
    }

    public void HandleClickingPath(PlayerHandle _player, PathObject _path)
    {
        if (currentPhase == Phase.CastlePlacement)
        {
            CastlePhaseClick(_player, _path);
        }
        else
        {
            UnitPhaseClick(_player, _path);
        }

        EndTurnForPlayer(_player);
    }

    void CastlePhaseClick(PlayerHandle _player, PathObject _path)
    {
        //Place castle at path location
        _player.NewUnitOnBoard(_path.transform.position, PlayerHandle.UnitType.Castle);        
    }

    void UnitPhaseClick(PlayerHandle _player, PathObject _path)
    {
        UnitBase unit = GameBoard.IsOccupied(_path.transform.position);
        switch (_path.GetPathType())
        {
            case PathObject.PathType.Spawn: 
                _player.NewUnitOnBoard(_path.transform.position, _player.GetCurrentWantedUnitType());
                break;
            case PathObject.PathType.Move:
                GameBoard.MoveOnBoard(_player.GetSelectedUnit(), _path.transform.position);
                break;
            case PathObject.PathType.Attack:
                _player.GetSelectedUnit().UnitAttacking(GameBoard.GetObjFromBoardPosition(_path.transform.position).GetComponent<UnitBase>());
                break;
        }        
    }
}
