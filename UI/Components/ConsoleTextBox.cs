using System.Collections.Generic;
using amulware.Graphics;
using NLog;
using System;

namespace TestClient.UI.Components
{
    class ConsoleTextBox : TextBox<string>
    {
        private readonly List<string> entries = new List<string>(); 
        private readonly Logger logger;

        public ConsoleTextBox(Bounds bounds, Logger logger)
            : base(bounds)
        {
            this.logger = logger;
        }
        
        protected override IReadOnlyList<string> GetItems()
        {
            entries.Clear();
            //logger.CopyRecentEntriesWithSeverity(lowestVisibleSeverity(), entries);
            // TODO - retrieve entries. NLog listener?
            return entries;
        }

        protected override Tuple<string, Color> Format(string item)
        {
            return new Tuple<string, Color>(item, Color.Red); // TODO - calc color
        }
    }
}
