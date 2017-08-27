using amulware.Graphics;
using TestClient.Rendering;
using TestClient.Utilities.Input;

namespace TestClient.UI.Components
{
    abstract class UIComponent
    {
        protected Bounds Bounds { get; }

        protected UIComponent(Bounds bounds)
        {
            Bounds = bounds;
        }

        public virtual void Update(UpdateEventArgs args) { }
        public virtual void HandleInput(InputContext input) { }
        public abstract void Draw(GeometryManager geometries);
    }
}
