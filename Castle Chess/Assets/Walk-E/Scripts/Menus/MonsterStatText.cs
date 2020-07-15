using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WalkEMons
{
    public class MonsterStatText : MonoBehaviour
    {
        Text txt;
        string originalTxt;
        IPenEntity being;
        public MonsterStatDisplay statToDisplay;
        public float refreshFrequency = 0.5f;

        void Init()
        {
            if(txt == null)
            {
                txt = GetComponent<Text>();
                originalTxt = txt.text;                
            }            
        }

        private IEnumerator UpdateText()
        {
            while (refreshFrequency > 0 && being != null)
            {
                UpdateStatText();
                yield return new WaitForSeconds(refreshFrequency);
            }
        }

        public void SetMonsterToDisplay(IPenEntity mon)
        {
            Init();
            being = mon;
            UpdateStatText();
            StartCoroutine(UpdateText());
        }

        public void UpdateStatText()
        {
            txt.text = originalTxt + being.GetTextRelatedToStat(statToDisplay);
        }

        public void TakeDown()
        {
            StopAllCoroutines();
            being = null;
        }
    }
}

