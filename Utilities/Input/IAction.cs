using System;

namespace TestClient.Utilities.Input
{
    interface IAction : IEquatable<IAction>
    {
        bool Hit { get; }
        bool Active { get; }
        bool Released { get; }

        bool IsAnalog { get; }
        float AnalogAmount { get; }
    }
}
