
namespace TestClient.Library
{
    public sealed class Box<T> where T : struct
    {
        public T Value { get; }

        public Box(T value)
        {
            Value = value;
        }
    }
}
