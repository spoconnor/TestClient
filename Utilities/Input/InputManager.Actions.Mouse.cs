﻿using System;
using System.Collections.Generic;
using System.Linq;
using TestClient.Utilities.Input.Actions;
using OpenTK.Input;

namespace TestClient.Utilities.Input
{
    partial class InputManager
    {
        public partial struct ActionConstructor
        {
            public MouseActions Mouse => new MouseActions(manager);
        }

        public struct MouseActions
        {
            private readonly InputManager manager;

            public MouseActions(InputManager inputManager)
            {
                manager = inputManager;
            }

            public IAction FromButton(MouseButton button) => new MouseButtonAction(manager, button);

            public IAction LeftButton => FromButton(MouseButton.Left);
            public IAction RightButton => FromButton(MouseButton.Right);
            public IAction MiddleButton => FromButton(MouseButton.Middle);

            public IEnumerable<IAction> GetAll()
            {
                var inputManager = manager;
                return ((MouseButton[]) Enum.GetValues(typeof(MouseButton)))
                    .Select(k => new MouseButtonAction(inputManager, k));
            }
        }
    }
}