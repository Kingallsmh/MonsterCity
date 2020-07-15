using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using WalkEMons;

namespace WalkEMons
{
    public class WalkEPlayer : MonoBehaviour
    {
        [SerializeField]
        PlayerState playerState;
        [SerializeField]
        PlayerMonsterPen playerMonsterPen;

        public void Init()
        {
            playerState = new PlayerState();
            playerMonsterPen = new PlayerMonsterPen();            
        }

        public void RegisterForManagerActions()
        {
            TimeManager.OnTimeTick += HandleTimeTick;
            EnergyManager.OnEnergyCollected += HandleEnergyCollect;
            AppManager.OnStartOfSave += OnSavePlayer;
        }

        private void OnDestroy()
        {
            TimeManager.OnTimeTick -= HandleTimeTick;
            EnergyManager.OnEnergyCollected -= HandleEnergyCollect;
        }

        public void HandleTimeTick(float secsPassed)
        {   
            playerState.timePlayed += secsPassed;
            playerMonsterPen.HandleTimeTick(secsPassed);
        }

        public void HandleEnergyCollect(int energy)
        {
            playerState.currentEnergy += EnergyManager.GetCurrentBuilt();
        }

        public MonsterBeing GetMonster(int numInArray)
        {
            return playerMonsterPen.GetMonsterFromPen(numInArray);
        }

        public PlayerMonsterPen GetMonsterPen()
        {
            return playerMonsterPen;
        }

        public void ReceiveNewEgg(MonsterEgg egg)
        {
            playerMonsterPen.SetEgg(egg);
        }

        #region Save and Load

        public void OnSavePlayer(PlayerFile file)
        {
            playerState.lastOn = DateTime.Now;
            file.player = playerState;
        }

        public PlayerState GetPlayerState()
        {            
            return playerState;
        }        

        public void SetPlayerFromFile(PlayerFile state)
        {
            if(state == null)
            {
                return;
            }
            playerState = state.player;            
            playerMonsterPen.SetPenState(state);
        }

        

        #endregion

        public string GetTextRelatedToStat(PlayerStatDisplay statText)
        {
            switch (statText)
            {
                case PlayerStatDisplay.Name:
                    return playerState.playerName;
                case PlayerStatDisplay.Energy:
                    return playerState.currentEnergy.ToString();
                case PlayerStatDisplay.TimePlayed:
                    return TimeManager.GetTimeString(playerState.timePlayed);
                case PlayerStatDisplay.NumOfMonsters:
                    return GetMonsterPen().GetMonsterList().Count.ToString();
            }
            return "";
        }    
    }

    [Serializable]
    public class PlayerState
    {
        public string playerName = "DEFAULT";
        public int currentEnergy = 0;
        public float timePlayed;
        public DateTime lastOn = DateTime.Now;

        public override string ToString()
        {
            StringBuilder build = new StringBuilder();
            build.Append("Name: ").Append(playerName).AppendLine();
            build.Append("Collected Energy: ").Append(currentEnergy).AppendLine();
            build.Append("Time Played: ").Append(TimeManager.GetTimeString(timePlayed)).AppendLine();
            build.Append("Last Played: ").Append(lastOn).AppendLine();
            return build.ToString();
        }
    }

    public enum PlayerStatDisplay
    {
        Name, Energy, TimePlayed, NumOfMonsters
    }
}