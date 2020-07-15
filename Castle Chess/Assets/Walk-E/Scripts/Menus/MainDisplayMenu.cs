using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalkEMons
{
    public class MainDisplayMenu : MonoBehaviour, UIScreenable
    {
        WalkEPlayer viewedPlayer;
        PlayerMonsterPen viewedPen;

        public EntityVisualizer monsterVisualPrefab;
        List<PenStateWatcher> watcherList;

        public List<PlayerStatText> statTextList;

        public Vector2 fieldRange = new Vector2(-10, 10);

        public void SetupUIScreen(WalkEPlayer player)
        {
            //Setup player related
            viewedPlayer = player;
            SetupPlayerUI();

            //Setup Player Monster related
            viewedPen = viewedPlayer.GetMonsterPen();
            SetupVisuals();

            //Set UI active
            gameObject.SetActive(true);

            viewedPen.OnNewPenEntity += CreateVisualizerFromIPenEntity;
            viewedPen.OnRemovedPenEntity += RemoveVisualizer;
        }

        void SetupVisuals()
        {
            if(watcherList != null)
            {
                for(int i = 0; i < watcherList.Count; i++)
                {
                    Destroy(watcherList[i].gameObject);
                }
            }
            watcherList = new List<PenStateWatcher>();
            for (int i = 0; i < viewedPen.GetListOfPenEntity().Count; i++)
            {
                CreateVisualizerFromIPenEntity(viewedPen.GetListOfPenEntity()[i]);
            }
        }

        void SetupPlayerUI()
        {
            for(int i = 0; i < statTextList.Count; i++)
            {
                statTextList[i].SetPlayerToDisplay(viewedPlayer);
            }
        }

        void CreateVisualizerFromIPenEntity(IPenEntity entity)
        {
            EntityVisualizer newVis = Instantiate(monsterVisualPrefab, transform);
            PenStateWatcher watch = newVis.gameObject.AddComponent<PenStateWatcher>();
            entity.SetupGameobjectWithPenComponents(newVis);
            entity.SetupEntityVisuals(newVis);
            watcherList.Add(watch);
            watch.Init(entity);
        }

        void RemoveVisualizer(IPenEntity entity)
        {
            for(int i = 0; i < watcherList.Count; i++)
            {
                if(entity == watcherList[i].GetCurrentWatchedEntity())
                {
                    PenStateWatcher watch = watcherList[i];
                    watcherList.RemoveAt(i);
                    Destroy(watch.gameObject);
                }
            }            
        }

        public void TakedownUIScreen()
        {
            //TODO: Maybe hold on to this screen info instead?
            viewedPen.OnPlayerMonsterPenChanged -= SetupVisuals;
            for (int i = watcherList.Count - 1; i > 0; i--)
            {
                Destroy(watcherList[i].gameObject);
            }
            for(int i = 0; i < statTextList.Count; i++)
            {
                statTextList[i].TakeDown();
            }
            gameObject.SetActive(false);
        }
    }
}

