﻿using System;
using System.Collections.Generic;
using amulware.Graphics;

namespace TestClient.UI.Components
{
    class InjectedTextBox<T> : TextBox<T>
    {
        private readonly Func<IReadOnlyList<T>> itemProvider;
        private readonly Func<T, Tuple<string, Color>> itemTransformer;

        public InjectedTextBox(Bounds bounds, Func<IReadOnlyList<T>> itemProvider, Func<T, Tuple<string, Color>> itemTransformer)
            : base(bounds)
        {
            this.itemProvider = itemProvider;
            this.itemTransformer = itemTransformer;
        }

        protected override IReadOnlyList<T> GetItems() => itemProvider();

        protected override Tuple<string, Color> Format(T item) => itemTransformer(item);
    }
}