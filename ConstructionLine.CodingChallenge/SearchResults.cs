using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    public class SearchResults
    {
        public IList<Shirt> Shirts { get; set; }

        public IList<SizeCount> SizeCounts { get; set; }

        public IList<ColorCount> ColorCounts { get; set; }
    }

    public class SizeCount
    {
        public Size Size { get; set; }

        public int Count { get; set; }
    }

    public class ColorCount
    {
        public Color Color { get; set; }

        public int Count { get; set; }
    }
}