using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalkEMons
{
    public class PenStateWatcher : MonoBehaviour
    {
        IPenEntity entity;

        public void Init(IPenEntity penEntity)
        {
            entity = penEntity;
            entity.RegisterForStateRequested(UpdatePenObjectState);
        }

        public void UpdatePenObjectState()
        {
            entity.SetPenLocation(transform.localPosition);
        }

        public IPenEntity GetCurrentWatchedEntity()
        {
            return entity;
        }

        private void OnDestroy()
        {
            UpdatePenObjectState();
            entity.UnregisterForStateRequested(UpdatePenObjectState);
        }
    }
}

