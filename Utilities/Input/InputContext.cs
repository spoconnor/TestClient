﻿using System;
using OpenTK;

namespace TestClient.Utilities.Input
{
    class InputContext
    {
        private readonly Lazy<Vector2> mousePosition;

        public InputManager Manager => State.InputManager;
        public InputState State { get; }
        public Vector2 MousePosition => mousePosition.Value;

        public InputContext(InputState inputState, Func<Vector2, Vector2> mouseTransformation)
        {
            State = inputState;
            mousePosition = new Lazy<Vector2>(() => mouseTransformation(inputState.InputManager.MousePosition));
        }
    }
}
