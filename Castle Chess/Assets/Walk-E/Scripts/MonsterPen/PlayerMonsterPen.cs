using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WalkEMons
{
    [Serializable]
    public class PlayerMonsterPen
    {
        //Is monster inventory for player
        [SerializeField]
        PlayerPenState penState;
        [SerializeField]
        List<MonsterBeing> playerMonsterList;
        [SerializeField]
        EggHatchery hatchery;

        public Action OnPlayerMonsterPenChanged = delegate { };
        public Action<IPenEntity> OnNewPenEntity = delegate { };
        public Action<IPenEntity> OnRemovedPenEntity = delegate { };

        public PlayerMonsterPen()
        {
            penState = new PlayerPenState();
            hatchery = new EggHatchery();
            hatchery.OnNewMonsterBorn += AddMonsterToPen;
            hatchery.OnEggAdded += AddPenEntity; //Careful with the delegate in delegate situation;
            hatchery.OnEggRemoved += RemovePenEntity;
            playerMonsterList = new List<MonsterBeing>();

            AppManager.OnStartOfSave += OnSavePen;
        }

        public void HandleTimeTick(float secsPassed = 1)
        {
            hatchery.HandleTimeTick(secsPassed);
            for (int i = 0; i < playerMonsterList.Count; i++)
            {
                playerMonsterList[i].HandleTimeTick(secsPassed);
            }
        }

        public MonsterBeing GetMonsterFromPen(int numInPen)
        {
            return playerMonsterList[numInPen];
        }

        public List<MonsterBeing> GetMonsterList()
        {
            return playerMonsterList;
        }

        public EggHatchery GetEggHatchery()
        {
            return hatchery;
        }

        public List<IPenEntity> GetListOfPenEntity()
        {
            List<IPenEntity> penList = new List<IPenEntity>();
            penList.AddRange(playerMonsterList);
            penList.AddRange(hatchery.GetMonsterEggList());
            return penList;
        }

        #region Monster Pen State Handle / Save and Load        

        public void SetEgg(MonsterEgg egg)
        {
            hatchery.AddEggToPen(egg);
        }

        public void AddPenEntity(IPenEntity entity)
        {
            OnNewPenEntity(entity);
        }

        public void RemovePenEntity(IPenEntity penEntity)
        {
            OnRemovedPenEntity(penEntity);
        }

        public void AddMonsterToPen(MonsterBeing monster)
        {
            playerMonsterList.Add(monster);
            penState.playersMonsters.Add(monster.GetUpdatedMonsterState());
            OnNewPenEntity(monster);
        }

        public void RemoveMonsterFromPen(MonsterBeing monster)
        {
            playerMonsterList.Remove(monster);
            penState.playersMonsters.Remove(monster.GetUpdatedMonsterState());
            OnRemovedPenEntity(monster);
        }

        public void OnSavePen(PlayerFile file)
        {
            file.monsterPen = GetPenState();
        }

        public PlayerPenState GetPenState()
        {
            UpdateMonsterStates();
            penState.hatcheryState = hatchery.GetHatcheryState();
            return penState;
        }

        void UpdateMonsterStates()
        {
            for (int i = 0; i < playerMonsterList.Count; i++)
            {
                playerMonsterList[i].GetUpdatedMonsterState();
            }
        }

        public void SetPenState(PlayerFile state)
        {
            penState = state.monsterPen;
            SetMonstersFromStateList();
            hatchery.SetHatcheryFromState(penState.hatcheryState);
        }

        void SetMonstersFromStateList()
        {
            for (int i = 0; i < penState.playersMonsters.Count; i++)
            {
                MonsterBeing mon = new MonsterBeing(penState.playersMonsters[i]);
                playerMonsterList.Add(mon);
            }
        }

        #endregion
    }

    [Serializable]
    public class PlayerPenState
    {
        public List<MonsterState> playersMonsters;
        public HatcheryState hatcheryState;

        public PlayerPenState()
        {
            playersMonsters = new List<MonsterState>();
            hatcheryState = new HatcheryState();
        }

        public override string ToString()
        {
            StringBuilder build = new StringBuilder();
            build.Append("Monster Pen: ").AppendLine();
            for (int i = 0; i < playersMonsters.Count; i++)
            {
                build.Append("#").Append(i).AppendLine();
                build.Append(playersMonsters[i].ToString()).AppendLine();
            }
            build.Append(hatcheryState).AppendLine();
            return build.ToString();
        }
    }

    [Serializable]
    public class PenObjectState
    {
        public SizedVector lastPos = new SizedVector();

        public override string ToString()
        {
            StringBuilder build = new StringBuilder();
            build.Append("Last Pos: ").Append(lastPos).AppendLine();
            return build.ToString();
        }
    }

    public interface IPenEntity
    {
        PenObjectState GetPenState();
        void SetPenLocation(Vector2 lastPos);
        void SetupGameobjectWithPenComponents(EntityVisualizer newVisual);
        void SetupEntityVisuals(EntityVisualizer newVisual);
        void RegisterForStateRequested(Action action);
        void UnregisterForStateRequested(Action action);
        string GetTextRelatedToStat(MonsterStatDisplay stat);
    }
}

