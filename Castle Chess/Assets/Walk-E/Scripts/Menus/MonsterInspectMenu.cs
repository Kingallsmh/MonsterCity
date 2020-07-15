using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

namespace WalkEMons
{
    public class MonsterInspectMenu : MonoBehaviour, UIScreenable
    {
        public WalkEPlayer player;
        int selectedBeing = 0;
        public EntityVisualizer visualizer;

        public MonsterStatText[] textArray;
        public Text textNumInList;
        string originalTxt;
        TimerObject switchAnimTimer;
        public float animTime = 1;
        int currentAnim = 0;

        bool isInitialized;

        void Init()
        {
            if (!isInitialized)
            {
                isInitialized = true;
                originalTxt = textNumInList.text;
            }            
        }

        public void SetupUIScreen(WalkEPlayer newPlayer)
        {
            Init();
            player = newPlayer;
            gameObject.SetActive(true);            
            SetupVisualizer(player.GetMonsterPen().GetListOfPenEntity()[selectedBeing]);
            switchAnimTimer = new TimerObject(animTime);            
        }

        public void TakedownUIScreen()
        {
            player = null;
            gameObject.SetActive(false);
            visualizer.TakeDownVisualizer();
        }

        private void Update()
        {
            if (player == null)//Temp keep update from errors
            {
                return;
            }
            if (switchAnimTimer.StepTimer())
            {
                if (currentAnim < visualizer.GetMonsterAnimList().Count - 1)
                {
                    currentAnim++;
                }
                else
                {
                    currentAnim = 0;
                }
                visualizer.SwitchAnimation(currentAnim);
                switchAnimTimer.ResetTimer();
            }
        }

        void SetupVisualizer(IPenEntity entity)
        {
            entity.SetupEntityVisuals(visualizer);

            for (int i = 0; i < textArray.Length; i++)
            {
                textArray[i].SetMonsterToDisplay(entity);
            }
            textNumInList.text = originalTxt + (selectedBeing+1);
        }

        public void BtnGetNextMonsterToInspect(int skipAmount)
        {
            selectedBeing += skipAmount;
            if (selectedBeing >= player.GetMonsterPen().GetListOfPenEntity().Count)
            {
                selectedBeing -= player.GetMonsterPen().GetListOfPenEntity().Count;
            }
            else if (selectedBeing < 0)
            {
                selectedBeing += player.GetMonsterPen().GetListOfPenEntity().Count;
            }
            visualizer.TakeDownVisualizer();
            SetupVisualizer(player.GetMonsterPen().GetListOfPenEntity()[selectedBeing]);
        }

        public void BtnExitInspect()
        {
            TakedownUIScreen();
        }        
    }
}

