using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    public class ColorCounter : PropertyCounter<Color>
    {
        public ColorCounter() : base(Color.All)
        {
        }

        public IList<ColorCount> GetColorCounts()
        {
            var colorCounts = new List<ColorCount>();

            foreach (var color in _countMap.Keys)
            {
                var actualCount = _countMap[color];
                var colorCount = new ColorCount { Count = actualCount, Color = color };
                colorCounts.Add(colorCount);
            }

            return colorCounts;
        }
    }
}