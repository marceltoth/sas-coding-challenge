using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    /// <summary>
    /// This Search Engine has O(1) space complexity and O(n) time complexity
    /// Search on 50k items runs in around 20ms. 
    /// It uses no caching, so is optimal for regular data updates and regular querying of up to 100k data items
    /// </summary>
    public class SearchEngine : ISearchEngine
    {
        private readonly IList<Shirt> _shirts;

        public SearchEngine(IList<Shirt> shirts)
        {
            _shirts = shirts;
        }

        public SearchResults Search(SearchOptions options)
        {
            var filterSizes = options.Sizes.Select(s => s.Id).ToList();
            var filterColors = options.Colors.Select(s => s.Id).ToList();

            var sizeCounter = new SizeCounter();
            var colorCounter = new ColorCounter();
            var matchingShirts = new List<Shirt>();

            foreach (var shirt in _shirts)
            {
                sizeCounter.Add(shirt.Size);
                colorCounter.Add(shirt.Color);

                bool isSizeMatch = filterSizes.Contains(shirt.Size.Id) || (filterSizes.Count == 0);
                bool isColorMatch = filterColors.Contains(shirt.Color.Id) || (filterColors.Count == 0);

                if (isColorMatch && isSizeMatch)
                {
                    matchingShirts.Add(shirt);
                }
            }

            return new SearchResults
            {
                Shirts = matchingShirts,
                SizeCounts = sizeCounter.GetSizeCounts(),
                ColorCounts = colorCounter.GetColorCounts()
            };
        }
    }
}