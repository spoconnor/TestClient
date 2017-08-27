﻿namespace TestClient.UI
{
    class FixedOffsetDimension : IDimension
    {
        private readonly IDimension parent;
        private readonly float offsetStart;
        private readonly float offsetEnd;

        public FixedOffsetDimension(IDimension parent, float offsetStart, float offsetEnd)
        {
            this.parent = parent;
            this.offsetStart = offsetStart;
            this.offsetEnd = offsetEnd;
        }

        public float Min => parent.Min + offsetStart;
        public float Max => parent.Max - offsetEnd;
    }
}