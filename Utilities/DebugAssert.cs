using System;
using System.Diagnostics;

namespace TestClient.Utilities
{
    static class DebugAssert
    {
        public static class Argument
        {
            [Conditional("DEBUG")]
            public static void IsNotNull<T>(T obj) where T : class
            {
                if (obj == null)
                    throw new ArgumentNullException();
            }

            [Conditional("DEBUG")]
            public static void Satisfies(bool condition)
            {
                if (!condition)
                    throw new ArgumentException();
            }
        }
    }
}
