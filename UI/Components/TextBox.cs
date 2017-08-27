using System.Collections.Generic;
using amulware.Graphics;
using TestClient.Rendering;
using OpenTK;
using System;

namespace TestClient.UI.Components
{
    abstract class TextBox<T> : UIComponent
    {
        protected TextBox(Bounds bounds)
            : base(bounds)
        {
        }

        protected abstract IReadOnlyList<T> GetItems();
        protected abstract Tuple<string, Color> Format(T item);

        public override void Draw(GeometryManager geometries)
        {
            var entries = GetItems();

            geometries.ConsoleFont.SizeCoefficient = new Vector2(1, 1);
            geometries.ConsoleFont.Height = Constants.UI.FontSize;

            var y = Bounds.YEnd - Constants.UI.LineHeight;
            var i = entries.Count;

            while (y >= -Constants.UI.LineHeight && i > 0)
            {
                var text = Format(entries[--i]);
                geometries.ConsoleFont.Color = text.Item2;
                geometries.ConsoleFont.DrawString(new Vector2(Bounds.XStart, y), text.Item1);
                y -= Constants.UI.LineHeight;
            }
        }
    }
}