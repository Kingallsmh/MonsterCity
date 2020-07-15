using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WalkEMons;

namespace WalkEMons
{
    public class MonsterDataBank : MonoBehaviour
    {
        static MonsterDataBank Instance;

        [Header("Required Objects")]
        public static Dictionary<string, MonsterTemplate> monsterTemplateBank = new Dictionary<string, MonsterTemplate>();
        public MonsterTemplate[] monsterList;

        private void Awake()
        {
            Instance = this;
            AddAllMonstersToBank();
        }

        public void AddAllMonstersToBank()
        {
            for (int i = 0; i < monsterList.Length; i++)
            {
                monsterTemplateBank.Add(monsterList[i].name, monsterList[i]);
            }
        }

        public static MonsterTemplate GetMonsterTemplate(string _speciesName)
        {
            if (monsterTemplateBank.ContainsKey(_speciesName))
            {
                return monsterTemplateBank[_speciesName];
            }
            else
            {
                return Instance.monsterList[0];
            }
        }

        public static MonsterTemplate GetRandomMonsterInRange(int min, int max)
        {
            min = Mathf.Clamp(min, 0, Instance.monsterList.Length);
            max = Mathf.Clamp(max, min, Instance.monsterList.Length);
            int rnd = Random.Range(min, max);
            return GetMonsterTemplate(Instance.monsterList[rnd].name);
        }

        public static MonsterTemplate GetRandomMonsterInRange()
        {
            return GetRandomMonsterInRange(0, Instance.monsterList.Length - 1);            
        }
    }
}

