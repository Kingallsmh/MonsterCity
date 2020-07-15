using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WalkEMons
{
    [Serializable]
    public class EggHatchery
    {
        [SerializeField]
        HatcheryState state;
        [SerializeField]
        List<MonsterEgg> eggList;

        public Action<MonsterBeing> OnNewMonsterBorn = delegate { };
        public Action<MonsterEgg> OnEggAdded = delegate { };
        public Action<MonsterEgg> OnEggRemoved = delegate { };

        public EggHatchery()
        {
            eggList = new List<MonsterEgg>();
            state = new HatcheryState();
        }

        public void HandleTimeTick(float secsPassed = 1)
        {
            for (int i = 0; i < eggList.Count; i++)
            {
                eggList[i].HandleTimeTick(secsPassed);
            }
        }

        public void HandleHatchedEgg(MonsterEgg egg)
        {
            MonsterBeing newBeing = MonsterManagement.Instance.CreateMonster(egg.GetSpeciesTemplate());
            newBeing.SetPenLocation(egg.GetUpdatedEggState().penState.lastPos.ReturnVector2());
            OnNewMonsterBorn(newBeing);
            RemoveEggFromPen(egg);
            egg = null;
        }

        public void AddEggToPen(MonsterEgg egg)
        {
            eggList.Add(egg);
            state.playersEggs.Add(egg.GetUpdatedEggState());
            egg.OnEggHatched += HandleHatchedEgg;
            OnEggAdded(egg);
        }

        public void RemoveEggFromPen(MonsterEgg egg)
        {
            eggList.Remove(egg);
            state.playersEggs.Remove(egg.GetUpdatedEggState());
            egg.OnEggHatched -= HandleHatchedEgg;
            OnEggRemoved(egg);
        }

        public HatcheryState GetHatcheryState()
        {
            UpdateEggStates();
            return state;
        }

        public void UpdateEggStates()
        {
            for (int i = 0; i < eggList.Count; i++)
            {
                eggList[i].GetUpdatedEggState();
            }
        }

        public void SetHatcheryFromState(HatcheryState newState)
        {
            state = newState;
            if (newState == null)
            {
                state = new HatcheryState();
            }
            SetEggsFromStateList();
        }

        void SetEggsFromStateList()
        {
            for (int i = 0; i < state.playersEggs.Count; i++)
            {
                MonsterEgg mon = new MonsterEgg(state.playersEggs[i]);
                eggList.Add(mon);
                mon.OnEggHatched += HandleHatchedEgg;
            }
        }

        public List<MonsterEgg> GetMonsterEggList()
        {
            return eggList;
        }
    }

    [Serializable]
    public class HatcheryState
    {
        public List<EggState> playersEggs;

        public HatcheryState()
        {
            playersEggs = new List<EggState>();
        }

        public override string ToString()
        {
            StringBuilder build = new StringBuilder();
            build.Append("Egg Hatchery: ").AppendLine();
            for (int i = 0; i < playersEggs.Count; i++)
            {
                build.Append("#").Append(i).AppendLine();
                build.Append(playersEggs[i].ToString()).AppendLine();
            }
            return build.ToString();
        }
    }
}

