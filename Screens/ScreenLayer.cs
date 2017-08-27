using amulware.Graphics;
using OpenTK;
using TestClient.Utilities.Input;
using System;
using TestClient.Rendering;

namespace TestClient.Screens
{
    interface IScreenLayer
    {
        void Update(UpdateEventArgs args);
        // Should return false if the input should not be propagated.
        bool HandleInput(UpdateEventArgs args, InputState inputState);
        void OnResize(ViewportSize newSize);
        void Render(RenderContext context);
    }

    abstract class ScreenLayer : IScreenLayer
    {
        private const float fovy = Math.PI / 2.0f;
        private const float zNear = .1f;
        private const float zFar = 1024f;

        protected readonly ScreenLayerCollection Parent;

        protected ViewportSize ViewportSize { get; private set; }
        protected bool Destroyed { get; private set; }

        public abstract Matrix4 ViewMatrix { get; }
        public virtual RenderOptions RenderOptions => RenderOptions.Default;

        public virtual Matrix4 ProjectionMatrix
        {
            get
            {
                var yMax = zNear * Math.Tan(.5f * fovy);
                var yMin = -yMax;
                var xMax = yMax * ViewportSize.AspectRatio;
                var xMin = yMin * ViewportSize.AspectRatio;
                return Matrix4.CreatePerspectiveOffCenter(xMin, xMax, yMin, yMax, zNear, zFar);
            }
        }

        protected ScreenLayer(ScreenLayerCollection parent)
        {
            Parent = parent;
        }

        public void OnResize(ViewportSize newSize)
        {
            ViewportSize = newSize;
            OnViewportSizeChanged();
        }

        public virtual bool HandleInput(UpdateEventArgs args, InputState inputState) => true;
        public abstract void Update(UpdateEventArgs args);
        public abstract void Draw();
        protected virtual void OnViewportSizeChanged() { }

        public void Render(RenderContext context)
        {
            context.Compositor.RenderLayer(this);
        }

        protected void Destroy()
        {
            Parent.RemoveScreenLayer(this);
            Destroyed = true;
        }
    }
}
