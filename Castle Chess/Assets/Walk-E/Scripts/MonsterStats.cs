using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Mathematics;
using UnityEngine;

namespace WalkEMons
{
    public enum StatType
    {
        MaxStamina, MaxHappiness, MaxHP, Attack, Luck
    }

    [Serializable]
    public class MonsterStats
    {
        /// <summary>
        /// Energy available to expend. Refills with food.
        /// </summary>
        public StatValue maxStamina = new StatValue(0);

        /// <summary>
        /// How happy the monster is.
        /// </summary>
        public StatValue maxHappiness = new StatValue(0);

        /// <summary>
        /// How tough the monster is. Refills with items or time being happy.
        /// </summary>
        public StatValue maxHP = new StatValue(0);

        /// <summary>
        /// How hard the monster hits;
        /// </summary>
        public StatValue attack = new StatValue(0);

        /// <summary>
        /// More luck = more chance to produce more favorable odds.
        /// </summary>
        public StatValue luck = new StatValue(0);

        

        [NonSerialized]
        public Dictionary<StatType, StatValue> statsDict = new Dictionary<StatType, StatValue>();

        public MonsterStats()
        {
            InitDictionnary();
        }

        public void InitDictionnary()
        {
            statsDict = new Dictionary<StatType, StatValue>()
                                            {
                {StatType.MaxStamina, maxStamina},
                {StatType.MaxHappiness, maxHappiness},
                {StatType.MaxHP, maxHP},
                {StatType.Attack, attack},
                {StatType.Luck, luck}
                                            };
        }       

        public void RegisterForStatChange(StatType type, Action<int> action)
        {
            statsDict[type].OnValueChanged += action;
        }

        public void UnregisterForStatChange(StatType type, Action<int> action)
        {
            statsDict[type].OnValueChanged += action;
        }

        #region Set and Get Stats

        public void AddStatsFromData(Dictionary<StatType, int> _data)
        {
            for (int i = 0; i < _data.Count; i++)
            {
                AddToStat(_data.ElementAt(i).Key, _data.ElementAt(i).Value);
            }
        }

        public void AddToStat(StatType _type, int _value)
        {
            if (statsDict.ContainsKey(_type))
            {
                statsDict[_type].AddToCurrent(_value);
            }
            else
            {
                Debug.LogError("Could not add to stat: " + _type);
            }
        }

        public void SetStats(Dictionary<StatType, StatValue> _statDictionnary)
        {
            for (int i = 0; i < _statDictionnary.Count; i++)
            {
                StatType key = _statDictionnary.ElementAt(i).Key;
                if (statsDict.ContainsKey(key))
                {
                    SetStat(key, _statDictionnary[key].currentValue);
                }
            }
        }

        public void SetStats(MonsterStats _data)
        {
            SetStats(_data.GetStatsDictionary());
        }

        public void SetStat(StatType _type, int _value)
        {
            statsDict[_type].SetCurrentValue(_value);
        }

        public int GetStat(StatType _type)
        {
            return statsDict[_type].currentValue;
        }

        public Dictionary<StatType, StatValue> GetStatsDictionary()
        {
            return statsDict;
        }

        public StatType GetRandomStat()
        {
            int rnd = UnityEngine.Random.Range(0, 5);
            return (StatType)rnd;
        }

        #endregion
    }
}

