using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    public class SizeCounter : PropertyCounter<Size>
    {
        public SizeCounter() : base(Size.All)
        {
        }

        public IList<SizeCount> GetSizeCounts()
        {
            var sizeCounts = new List<SizeCount>();
            foreach (var size in _countMap.Keys)
            {
                var actualCount = _countMap[size];
                var sizeCount = new SizeCount { Count = actualCount, Size = size };
                sizeCounts.Add(sizeCount);
            }

            return sizeCounts;
        }
    }
}