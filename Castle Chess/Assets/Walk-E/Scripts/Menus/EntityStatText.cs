using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WalkEMons
{
    [RequireComponent(typeof(Text))]
    public class EntityStatText : MonoBehaviour
    {
        Text txt;
        string originalTxt;
        IStatTextable entity;
        public string statToDisplay;
        public float refreshFrequency = 0.5f;

        void Init()
        {
            if (txt == null)
            {
                txt = GetComponent<Text>();
                originalTxt = txt.text;
            }
        }

        private IEnumerator UpdateText()
        {
            while (refreshFrequency > 0 && entity != null)
            {
                UpdateStatText();
                yield return new WaitForSeconds(refreshFrequency);
            }
        }

        //public void SetMonsterToDisplay(MonsterBeing mon)
        //{
        //    Init();
        //    being = mon;
        //    UpdateStatText();
        //    StartCoroutine(UpdateText());
        //}

        public void UpdateStatText()
        {
            txt.text = originalTxt + entity.GetTextRelatedToStat(statToDisplay);
        }

        public void TakeDown()
        {
            StopAllCoroutines();
            entity = null;
        }
    }

    public interface IStatTextable
    {
        string GetTextRelatedToStat(string statToDisplay);
    }
}

