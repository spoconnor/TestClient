﻿using System.Collections.Generic;
using amulware.Graphics;
using TestClient.Rendering;
using TestClient.Utilities.Input;
using TestClient.Utilities;
using OpenTK;
using OpenTK.Input;
using TestClient.Library;

namespace TestClient.UI.Components
{
    class TextInput : FocusableUIComponent
    {
        private static readonly HashSet<char> allowedChars = new HashSet<char> {' ', '-', '_', '.', '+', '"'};
        private const string cursorString = "|";

        private int cursorPosition;
        private string text = "";

        public string Text
        {
            get { return text; }
            set
            {
                text = value ?? "";
                cursorPosition = text.Length;
            }
        }

        public event GenericEventHandler<string> Submitted; 

        public TextInput(Bounds bounds) : base(bounds)
        {
        }

        public override void HandleInput(InputContext input)
        {
            if (!IsFocused)
            {
                return;
            }
            if (input.Manager.IsKeyHit(Key.Enter))
            {
                Submitted?.Invoke(text);
                return;
            }
            if (input.Manager.IsKeyHit(Key.BackSpace) && cursorPosition > 0)
            {
                text = text.Substring(0, cursorPosition - 1) + text.Substring(cursorPosition);
                cursorPosition--;
            }
            if (input.Manager.IsKeyPressed(Key.Left) && cursorPosition > 0)
            {
                cursorPosition--;
            }
            if (input.Manager.IsKeyPressed(Key.Right) && cursorPosition < text.Length)
            {
                cursorPosition++;
            }
            if (input.Manager.IsKeyPressed(Key.Home))
            {
                cursorPosition = 0;
            }
            if (input.Manager.IsKeyPressed(Key.End))
            {
                cursorPosition = text.Length;
            }

            foreach (var c in input.State.PressedCharacters)
            {
                if ((c < '0' || c > '9') && (c < '@' || c > 'Z') && (c < 'a' || c > 'z') && !allowedChars.Contains(c))
                {
                    continue;
                }

                text = text.Substring(0, cursorPosition) + c + text.Substring(cursorPosition);
                cursorPosition++;
            }
        }

        public override void Draw(GeometryManager geometries)
        {
            geometries.ConsoleFont.Height = Constants.UI.FontSize;
            geometries.ConsoleFont.Color = Color.White;

            var height = Bounds.YStart + .5f * Bounds.Height;
            geometries.ConsoleFont.DrawString(new Vector2(Bounds.XStart, height), text, 0, .5f);

            if (!IsFocused)
            {
                return;
            }

            var cursorXOffset = Bounds.XStart + geometries.ConsoleFont.StringWidth(text.Substring(0, cursorPosition));
            geometries.ConsoleFont.DrawString(new Vector2(cursorXOffset, height), cursorString, .5f, .5f);
        }
    }
}