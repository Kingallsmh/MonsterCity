using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WalkEMons
{
    public class PlayerStatText : MonoBehaviour
    {
        WalkEPlayer player;
        Text txt;
        string originalTxt;
        public PlayerStatDisplay statToDisplay;

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
            while(refreshFrequency > 0 && player != null)
            {
                UpdateStatText();
                yield return new WaitForSeconds(refreshFrequency);
            }
        }

        public void SetPlayerToDisplay(WalkEPlayer p)
        {
            Init();
            player = p;
            UpdateStatText();
            StartCoroutine(UpdateText());
        }

        public void UpdateStatText()
        {
            txt.text = originalTxt + player.GetTextRelatedToStat(statToDisplay);
        }

        public void TakeDown()
        {
            StopAllCoroutines();
            player = null;
        }
    }
}

