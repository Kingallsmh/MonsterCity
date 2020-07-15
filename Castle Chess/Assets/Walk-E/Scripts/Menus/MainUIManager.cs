using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace WalkEMons
{
    public class MainUIManager : MonoBehaviour
    {
        public static MainUIManager Instance;

        public MainDisplayMenu mainMenu;
        public MonsterInspectMenu inspectMenu;

        private void Awake()
        {
            AppManager.OnPlayerLoadedIn += StartUI;
        }

        void StartUI()
        {
            mainMenu.SetupUIScreen(AppManager.appPlayer);
        }

        public void TempShowInspectMenu()
        {
            inspectMenu.SetupUIScreen(AppManager.appPlayer);
        }
    }

    public interface UIScreenable
    {
        void SetupUIScreen(WalkEPlayer player);
        void TakedownUIScreen();
    }
}

