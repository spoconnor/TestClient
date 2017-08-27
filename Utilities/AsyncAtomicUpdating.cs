
namespace TestClient.Utilities
{ 
    class AsyncAtomicUpdating<T> where T : struct
    {
        public T Current { get; private set; }
        public T Previous { get; private set; }
        private object lastRecorded = new object(default(T));

        public void SetLastKnownState(T state)
        {
            lastRecorded = state;
        }

        public void Update() => UpdateTo(lastRecorded.Value);

        public void UpdateToDefault() => UpdateTo(default(T));

        public void UpdateTo(T state)
        {
            Previous = Current;
            Current = state;
        }
    }
}

