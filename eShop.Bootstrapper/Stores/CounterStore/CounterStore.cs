using System;

namespace eShop.Bootstrapper.Stores.CounterStore
{
    public class CounterState
    {
        public int Count { get; }

        public CounterState(int count)
        {
            Count = count;
        }
    }

    public class CounterStore
    {
        private CounterState state;

        public CounterStore()
        {
            state = new CounterState(0);
        }

        public CounterState GetState()
        {
            return state;
        }

        public void IncrementCount()
        {
            var count = state.Count;
            count++;
            state = new CounterState(count);
            BroadcastStateChange();
        }

        public void DecrementCount()
        {
            var count = state.Count;
            count--;
            state = new CounterState(count);
            BroadcastStateChange();
        }

        #region observer pattern

        private Action listeners;

        public void AddStateChangeListeners(Action listener)
        {
            listeners += listener;
        }

        public void RemoveStateChangeListeners(Action listener)
        {
            listeners -= listener;
        }

        private void BroadcastStateChange()
        {
            listeners.Invoke();
        }

        #endregion observer pattern
    }
}