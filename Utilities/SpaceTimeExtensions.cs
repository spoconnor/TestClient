using System;


namespace TestClient.Utilities
{
    static class SpaceTimeExtensions
    {
        public static TimeSpan S(this int val) => new TimeSpan(val);
        public static TimeSpan S(this double val) => new TimeSpan((long)val);
    }
}
