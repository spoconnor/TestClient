﻿using amulware.Graphics;
using TestClient.Utilities.Input;

namespace TestClient.Screens
{
    abstract class ScreenLayerGroup : ScreenLayerCollection, IScreenLayer
    {
        private readonly ScreenLayerCollection parent;

        protected ScreenLayerGroup(ScreenLayerCollection parent)
        {
            this.parent = parent;
        }

        public void Update(UpdateEventArgs args)
        {
            UpdateAll(args);
        }

        public bool HandleInput(UpdateEventArgs args, InputState inputState)
        {
            return PropagateInput(args, inputState);
        }

        protected void Destroy()
        {
            parent.RemoveScreenLayer(this);
        }
    }
}