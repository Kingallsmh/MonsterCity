using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WalkEMons
{
    [Serializable]
    public class MonsterEgg: IPenEntity
    {
        [SerializeField]
        MonsterTemplate monsterToBeBorn;
        [SerializeField]
        EggState state;
        public Action OnStateRequested = delegate { };

        public Action<MonsterEgg> OnEggHatched = delegate { };

        public MonsterEgg()
        {
            state = new EggState();
        }

        public MonsterEgg(MonsterTemplate toBirth)
        {
            monsterToBeBorn = toBirth;
            state = new EggState();
            state.speciesToBeBorn = toBirth.name;
        }

        public MonsterEgg(EggState newState)
        {
            state = newState;
            monsterToBeBorn = MonsterDataBank.GetMonsterTemplate(state.speciesToBeBorn);
        }

        public void HandleTimeTick(float time)
        {
            state.timeAlive += time;
            HandleHatchingEgg();
        }

        public void HandleHatchingEgg()
        {
            if(state.timeAlive >= monsterToBeBorn.hatchTime)
            {
                OnEggHatched(this);
            }
        }

        public MonsterTemplate GetSpeciesTemplate()
        {
            return monsterToBeBorn;
        }

        #region Save and Load

        public EggState GetUpdatedEggState()
        {
            state.speciesToBeBorn = monsterToBeBorn.name;
            OnStateRequested?.Invoke();
            return state;
        }

        public void SetEggFromState(EggState newState)
        {
            state = newState;
            monsterToBeBorn = MonsterDataBank.GetMonsterTemplate(state.speciesToBeBorn);
        }

        

        #endregion

        public string GetTextRelatedToStat(MonsterStatDisplay stat)
        {
            switch (stat)
            {
                case MonsterStatDisplay.Name: return "Egg";
                case MonsterStatDisplay.Species: return "?";
                case MonsterStatDisplay.TimeAlive: return TimeManager.GetTimeString(state.timeAlive);
                case MonsterStatDisplay.Level: return "?";
                case MonsterStatDisplay.Exp: return "?";
                case MonsterStatDisplay.Happiness: return "?";
                case MonsterStatDisplay.Stamina: return "?";
                case MonsterStatDisplay.HP: return "?";
                case MonsterStatDisplay.Attack: return "?";
                case MonsterStatDisplay.Luck: return "?"; 
            }
            return "";
        }

        public PenObjectState GetPenState()
        {
            return state.penState;
        }

        public void SetPenLocation(Vector2 lastPos)
        {
            state.penState.lastPos = new SizedVector(lastPos);
        }

        public void SetupGameobjectWithPenComponents(EntityVisualizer newVisual)
        {
            if (state.penState.lastPos.valueArray.Length > 0)
            {
                newVisual.transform.localPosition = state.penState.lastPos.ReturnVector2();
            }
            else
            {
                newVisual.transform.localPosition = Vector2.zero;
            }
            CircleCollider2D col = newVisual.GetComponent<CircleCollider2D>();
            col.offset = new Vector2(0, monsterToBeBorn.colRadius);
            col.radius = monsterToBeBorn.colRadius;
        }

        public void SetupEntityVisuals(EntityVisualizer newVisual)
        {
            newVisual.Init(GetSpeciesTemplate().eggAnimation);
        }

        public void RegisterForStateRequested(Action action)
        {
            OnStateRequested += action;
        }

        public void UnregisterForStateRequested(Action action)
        {
            OnStateRequested -= action;
            if (OnStateRequested == null)
            {
                OnStateRequested = delegate { };
            }
        }

        
    }

    [Serializable]
    public class EggState
    {
        public string speciesToBeBorn;
        public float timeAlive;
        public PenObjectState penState = new PenObjectState();        

        public override string ToString()
        {
            StringBuilder build = new StringBuilder();
            build.Append("Species of Egg: ").Append(speciesToBeBorn).AppendLine();
            build.Append("Time: ").Append(timeAlive).AppendLine();
            build.Append(penState).AppendLine();
            return build.ToString();
        }
    }
}

