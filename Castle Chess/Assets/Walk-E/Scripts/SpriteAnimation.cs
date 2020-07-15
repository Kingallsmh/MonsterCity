using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalkEMons
{
    [Serializable]
    public class SpriteAnimation
    {
        public string animationName;
        public SpriteFrame[] spriteFrameArray;

        public int GetAnimationFrames()
        {
            if (spriteFrameArray == null) { return 0; }
            return spriteFrameArray.Length;
        }

        public SpriteFrame GetSpriteFrame(int frame = 0)
        {
            if(frame >= 0 && frame < spriteFrameArray.Length)
            {
                return spriteFrameArray[frame];
            }
            else
            {
                return spriteFrameArray[0];
            }
        }
    }

    [Serializable]
    public class SpriteFrame
    {
        public float frameLength = 0.1f;
        public Sprite frameImg;
    }
}

