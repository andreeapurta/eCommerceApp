using eShop.UseCases.Interfaces.StateStore;
using System;

namespace eShop.StateStore.LocalStorage
{
    public class StateStoreBase : IStateStore
    {
        protected Action listeners;

        public void AddStateChangeListeners(Action listener)
        {
            listeners += listener;
        }

        public void BroadCastStateChange()
        {
            if (listeners != null)
            {
                listeners.Invoke();
            }
        }

        public void RemoveStateChangeListeners(Action listener)
        {
            listeners -= listener;
        }
    }
}