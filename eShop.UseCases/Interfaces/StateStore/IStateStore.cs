using System;

namespace eShop.UseCases.Interfaces.StateStore
{
    public interface IStateStore
    {
        void AddStateChangeListeners(Action listener);

        void RemoveStateChangeListeners(Action listener);

        void BroadCastStateChange();
    }
}