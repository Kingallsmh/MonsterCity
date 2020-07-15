using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using WalkEMons;

namespace WalkEMons
{
    [Serializable]
    public class MonsterBeing : IPenEntity
    {
        [SerializeField]
        MonsterState state;
        public Action OnMonsterStateRequested = delegate { };
        [SerializeField]
        MonsterTemplate monsterSpecies;

        public MonsterBeing(MonsterTemplate species)
        {            
            monsterSpecies = species;
            state = new MonsterState();
            state.name = monsterSpecies.name;
            state.gainedStats = new MonsterStats();
        }

        public MonsterBeing(MonsterState state)
        {
            SetBeingFromState(state);
        }

        public void HandleTimeTick(float secsPassed)
        {
            state.timeAlive += secsPassed;
        }        

        #region Save and Load

        public MonsterState GetUpdatedMonsterState()
        {            
            state.species = monsterSpecies.name;
            Debug.Log(OnMonsterStateRequested == null);
            OnMonsterStateRequested?.Invoke();
            return state;
        }

        public void SetBeingFromState(MonsterState state)
        {
            this.state = state;
            this.state.gainedStats.InitDictionnary();
            monsterSpecies = MonsterDataBank.GetMonsterTemplate(state.species);
        }

        #endregion        

        #region Stats

        public int GetStat(StatType type)
        {
            return monsterSpecies.defaultStats.GetStat(type) + state.gainedStats.GetStat(type);
        }

        public void AddToStat(StatType type, int num)
        {
            state.gainedStats.AddToStat(type, num);
        }

        public void SetCurrentToMax(CurrentStat stat)
        {
            SetCurrentAmount(stat, 100);
        }

        public void AddToCurrentAmount(CurrentStat stat, int amount)
        {
            SetCurrentAmount(stat, GetCurrentAmount(stat) + amount);
        }

        public void SetCurrentAmount(CurrentStat stat, int amount)
        {
            switch (stat)
            {
                case CurrentStat.Level: state.lvl.SetCurrentValue(amount); 
                    break;
                case CurrentStat.Exp: state.exp.SetCurrentValue(amount); 
                    break;
                case CurrentStat.Happiness: state.happiness.SetCurrentValue(amount); 
                    if(state.happiness.currentValue > GetStat(StatType.MaxHappiness))
                    {
                        state.happiness.SetCurrentValue(GetStat(StatType.MaxHappiness));
                    }
                    break;
                case CurrentStat.Stamina: state.stamina.SetCurrentValue(amount);
                    if (state.stamina.currentValue > GetStat(StatType.MaxStamina))
                    {
                        state.stamina.SetCurrentValue(GetStat(StatType.MaxStamina));
                    }
                    break;
                case CurrentStat.HP: state.HP.SetCurrentValue(amount);
                    if (state.HP.currentValue > GetStat(StatType.MaxHP))
                    {
                        state.HP.SetCurrentValue(GetStat(StatType.MaxHP));
                    }
                    break;
            }
        }

        public int GetCurrentAmount(CurrentStat stat)
        {
            return GetStatValue(stat).currentValue;
        }

        StatValue GetStatValue(CurrentStat stat)
        {
            switch (stat)
            {
                case CurrentStat.Level: return state.lvl;
                case CurrentStat.Exp: return state.exp;
                case CurrentStat.Happiness: return state.happiness;
                case CurrentStat.Stamina: return state.stamina;
                case CurrentStat.HP: return state.HP;
            }
            Debug.LogError("Not a stat?");
            return new StatValue(0);
        }

        public string GetTextRelatedToStat(MonsterStatDisplay stat)
        {
            switch (stat)
            {
                case MonsterStatDisplay.Name: return state.name;
                case MonsterStatDisplay.Species: return state.species;
                case MonsterStatDisplay.TimeAlive: return TimeManager.GetTimeString(state.timeAlive);
                case MonsterStatDisplay.Level: return state.lvl.ToString();
                case MonsterStatDisplay.Exp: return state.exp.ToString();
                case MonsterStatDisplay.Happiness: return GetCurrentAmount(CurrentStat.Happiness) + "/" + GetStat(StatType.MaxHappiness);
                case MonsterStatDisplay.Stamina: return GetCurrentAmount(CurrentStat.Stamina) + "/" + GetStat(StatType.MaxStamina);
                case MonsterStatDisplay.HP: return GetCurrentAmount(CurrentStat.HP) + "/" + GetStat(StatType.MaxHP);
                case MonsterStatDisplay.Attack: return GetStat(StatType.Attack).ToString();
                case MonsterStatDisplay.Luck: return GetStat(StatType.Luck).ToString();
            }
            return "";
        }

        public void RegisterForCurrentStatChange(CurrentStat txt, Action<int> action)
        {
            GetStatValue(txt).OnValueChanged += action;
        }

        public void UnregisterForCurrentStatChange(CurrentStat txt, Action<int> action)
        {
            GetStatValue(txt).OnValueChanged -= action;
        }

        #endregion              

        #region IPenEntity

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
            newVisual.gameObject.AddComponent<MovementBehaviour>();
            CircleCollider2D col = newVisual.GetComponent<CircleCollider2D>();
            col.offset = new Vector2(0, monsterSpecies.colRadius);
            col.radius = monsterSpecies.colRadius;
        }

        public void SetupEntityVisuals(EntityVisualizer newVisual)
        {
            newVisual.Init(GetAnimationList());
        }

        public void RegisterForStateRequested(Action action)
        {
            OnMonsterStateRequested += action;
        }

        public void UnregisterForStateRequested(Action action)
        {
            OnMonsterStateRequested -= action;            
            if(OnMonsterStateRequested == null)
            {
                OnMonsterStateRequested = delegate { };
            }
            Debug.Log(OnMonsterStateRequested == null);
        }

        #endregion

        public MonsterTemplate GetMonsterTemplate()
        {
            return monsterSpecies;
        }

        public List<SpriteAnimation> GetAnimationList()
        {
            List<SpriteAnimation> animList = new List<SpriteAnimation>();
            animList.Add(GetMonsterTemplate().walkAnimation);
            animList.Add(GetMonsterTemplate().attackAnimation);
            return animList;
        }

        
    }

    [Serializable]
    public class MonsterState
    {
        public string name;
        public string species;
        public float timeAlive;
        public StatValue lvl = new StatValue(0);
        public StatValue exp = new StatValue(0);
        public StatValue happiness = new StatValue(0);
        public StatValue stamina = new StatValue(0);
        public StatValue HP = new StatValue(0);
        public MonsterStats gainedStats;
        public PenObjectState penState = new PenObjectState();       

        public override string ToString()
        {
            StringBuilder build = new StringBuilder();
            build.Append("Nickname: ").Append(name).AppendLine();
            build.Append("Species: ").Append(species).AppendLine();
            build.Append("Time Alive: ").Append(TimeManager.GetTimeString(timeAlive)).AppendLine();
            build.Append("Level: ").Append(lvl).AppendLine();
            build.Append("Exp: ").Append(exp).AppendLine();
            build.Append("Happiness: ").Append(gainedStats.maxHappiness).AppendLine();
            build.Append("Stamina: ").Append(gainedStats.maxStamina).AppendLine();
            build.Append("HP: ").Append(gainedStats.maxHP).AppendLine();
            build.Append("Attack: ").Append(gainedStats.attack).AppendLine();
            build.Append("Luck: ").Append(gainedStats.luck).AppendLine();
            build.Append(penState).AppendLine();
            return build.ToString();
        }
    }

    public enum CurrentStat
    {
        Level, Exp, Happiness, Stamina, HP
    }

    public enum MonsterStatDisplay
    {
        Name, Species, TimeAlive, Level, Exp, Happiness, Stamina, HP, Attack, Luck
    }
}

