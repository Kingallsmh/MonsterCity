using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using WalkEMons;

namespace WalkEMons
{
    public class AppManager : MonoBehaviour
    {
        public static WalkEPlayer appPlayer;
        public WalkEPlayer debugPlayer;
        PlayerFile saveFileInfo;

        public static Action<PlayerFile> OnStartOfSave = delegate { };
        public static Action OnPlayerLoadedIn = delegate { };

        private void Start()
        {
            saveFileInfo = new PlayerFile();
            if (debugPlayer)
            {
                appPlayer = debugPlayer;
                appPlayer.RegisterForManagerActions();
            }
            else
            {
                GameObject player = new GameObject("Player");
                appPlayer = player.AddComponent<WalkEPlayer>();
                appPlayer.Init();
                appPlayer.RegisterForManagerActions();
                LoadPlayerFile();
            }            
            
            OnPlayerLoadedIn();
        }

        private void OnDisable()
        {
            SavePlayerFile();
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                SavePlayerFile();
            }            
        }

        public void SavePlayerFile()
        {
            OnStartOfSave(saveFileInfo);
            DataManagement.SavePlayer(saveFileInfo);
        }
        
        public void LoadPlayerFile()
        {
            if (DataManagement.FileExists())
            {
                saveFileInfo = DataManagement.LoadPlayer();
                if(saveFileInfo != null)
                {
                    appPlayer.SetPlayerFromFile(saveFileInfo);
                    TimeManager.Instance.AddTicksSinceLastOn(appPlayer.GetPlayerState().lastOn);
                }
            }
            else
            {
                MonsterManagement.Instance.CreateNewRandomEgg();
                Debug.Log("No Save File");
            }
        }
    }

    [Serializable]
    public class PlayerFile
    {
        public PlayerState player;
        public PlayerPenState monsterPen;

        public override string ToString()
        {
            StringBuilder build = new StringBuilder();
            build.Append(player.ToString()).AppendLine();
            build.Append(monsterPen.ToString()).AppendLine();
            return build.ToString();
        }
    }
}

