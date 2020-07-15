using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalkEMons
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class EntityVisualizer : MonoBehaviour
    {
        SpriteRenderer rend;
        SpriteAnimHandle animHandle;
        Dictionary<string, SpriteAnimation> animDict;
        List<SpriteAnimation> animList;

        bool isInitialized = false;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            if (!isInitialized)
            {
                rend = GetComponent<SpriteRenderer>();
                animHandle = new SpriteAnimHandle();
                animDict = new Dictionary<string, SpriteAnimation>();
                animList = new List<SpriteAnimation>();
                isInitialized = true;
            }            
        }

        public void Init(List<SpriteAnimation> animList)
        {
            Init();
            for(int i = 0; i < animList.Count; i++)
            {
                AddMonsterAnimationToCollections(animList[i]);
            }
        }

        public void Init(SpriteAnimation anim)
        {
            Init();
            AddMonsterAnimationToCollections(anim);            
        }

        public void AddMonsterAnimationToCollections(SpriteAnimation anim)
        {
            animDict.Add(anim.animationName, anim);
            animList.Add(anim);
            if(animList.Count == 1)
            {
                animHandle.SetMonsterAnim(animList[0]);
                rend.sprite = animHandle.GetCurrentImg();
            }
        }

        public void TakeDownVisualizer()
        {
            animHandle = new SpriteAnimHandle();
            animDict = new Dictionary<string, SpriteAnimation>();
            animList = new List<SpriteAnimation>();
        }

        void Update()
        {
            if (animHandle != null)
            {
                if (animHandle.UpdateAnimation(Time.deltaTime))
                {
                    rend.sprite = animHandle.GetCurrentImg();
                }
            }
        }

        public bool SwitchAnimtaion(string newAnimation)
        {
            if (animDict.ContainsKey(newAnimation))
            {
                animHandle.SetMonsterAnim(animDict[newAnimation]);
                return true;
            }
            return false;
        }

        public void SwitchAnimation(int num)
        {
            if (num < animList.Count && num >= 0)
            {
                animHandle.SetMonsterAnim(animList[num]);
            }
        }

        public List<SpriteAnimation> GetMonsterAnimList()
        {
            return animList;
        }
    }

    public class SpriteAnimHandle
    {
        SpriteAnimation currentAnim;
        SpriteFrame currentFrame;
        int currentFrameNum = 0;
        float currentTime = 0;

        public void SetMonsterAnim(SpriteAnimation anim)
        {
            currentAnim = anim;
            currentFrame = currentAnim.GetSpriteFrame(currentFrameNum);
        }

        public bool UpdateAnimation(float time)
        {
            if (currentTime >= currentFrame.frameLength)
            {
                NextFrame();
                currentTime = 0;
                return true;
            }
            currentTime += time;
            return false;
        }

        public void NextFrame()
        {
            if (currentFrameNum < currentAnim.GetAnimationFrames() - 1)
            {
                currentFrameNum++;
            }
            else
            {
                currentFrameNum = 0;
            }
            currentFrame = currentAnim.GetSpriteFrame(currentFrameNum);
        }

        public Sprite GetCurrentImg()
        {
            return currentFrame.frameImg;
        }
    }
}

