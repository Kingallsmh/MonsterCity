using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalkEMons
{
    public class MonsterPenManager : MonoBehaviour
    {
        public static MonsterPenManager Instance;
        WalkEPlayer player;

        private void Awake()
        {
            Instance = this;
        }

        
    }
}

