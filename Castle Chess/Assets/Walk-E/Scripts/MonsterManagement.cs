using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalkEMons
{
    public class MonsterManagement : MonoBehaviour
    {
        public static MonsterManagement Instance;

        private void Awake()
        {
            Instance = this;
        }

        public MonsterBeing CreateMonster(MonsterTemplate template, int level = 1)
        {
            MonsterBeing newMon = new MonsterBeing(template);
            newMon.GetUpdatedMonsterState().lvl.SetCurrentValue(level);
            newMon.GetUpdatedMonsterState().gainedStats = GetRandomizedStats(level);
            newMon.SetCurrentToMax(CurrentStat.HP);
            newMon.SetCurrentToMax(CurrentStat.Stamina);
            return newMon;
        }

        public MonsterBeing CreateMonster(string speciesName, int level = 1)
        {
            MonsterBeing newMon = CreateMonster(MonsterDataBank.GetMonsterTemplate(speciesName), level);            
            return newMon;
        }

        MonsterStats GetRandomizedStats(int level)
        {
            MonsterStats stats = new MonsterStats();
            for(int i = 0; i < level; i++)
            {
                AddToRandomStat(stats);
            }
            return stats;
        }

        void AddToRandomStat(MonsterStats stats, int amount = 1)
        {
            StatType type = stats.GetRandomStat();
            stats.AddToStat(type, amount);
        }

        //FOR DEBUG PURPOSES
        public void CreateNewRandomEgg()
        {
            MonsterEgg egg = new MonsterEgg(MonsterDataBank.GetRandomMonsterInRange());
            egg.GetPenState().lastPos = new SizedVector(new Vector2(Random.Range(-50, 50), Random.Range(-50, 50)));
            AppManager.appPlayer.ReceiveNewEgg(egg);
        }
    }
}

