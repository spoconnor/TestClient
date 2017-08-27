/*
using System.Collections.Generic;
using amulware.Graphics;
using TestClient.Meta;
using TestClient.Library;

namespace TestClient.UI.Components
{
    class ConsoleTextBox : TextBox<Logger.Entry>
    {
        private readonly List<Logger.Entry> entries = new List<Logger.Entry>(); 
        private readonly Logger logger;

        public ConsoleTextBox(Bounds bounds, Logger logger)
            : base(bounds)
        {
            this.logger = logger;
        }
        
        protected override IReadOnlyList<Logger.Entry> GetItems()
        {
            entries.Clear();
            logger.CopyRecentEntriesWithSeverity(lowestVisibleSeverity(), entries);
            return entries;
        }

        protected override (string, Color) Format(Logger.Entry item)
            => (item.Text, colors[item.Severity]);
    }
}
*/
