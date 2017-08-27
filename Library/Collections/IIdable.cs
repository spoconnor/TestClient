
namespace TestClient.Library.Collections
{
    public interface IIdable<T>
    {
        Id<T> Id { get; }
    }
}
