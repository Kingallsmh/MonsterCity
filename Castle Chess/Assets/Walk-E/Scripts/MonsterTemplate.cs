using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalkEMons
{
    [CreateAssetMenu(fileName = "New MonsterTemplate", menuName = "MonsterTemplate", order = 51)]
    public class MonsterTemplate : ScriptableObject
    {
        public float colRadius = 4;
        
        [Header("Default Monster Stats")]
        public MonsterStats defaultStats;

        [Header("Monster Animations")]
        public SpriteAnimation walkAnimation;
        public SpriteAnimation attackAnimation;

        [Header("Monster Growth")]
        public SpriteAnimation eggAnimation;
        public float hatchTime = 300;

        
    }
}

