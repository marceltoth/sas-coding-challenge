using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    /// <summary>
    /// This Search Engine trades the memory consumption and write performance for query (read) performance
    /// Search on 500k items runs in around 20ms. 
    /// </summary>
    public class SearchEngineWithCache : ISearchEngine
    {
        private readonly IList<Shirt> _shirts;
        private readonly Dictionary<Color, HashSet<Shirt>> _colorSetsDictionary;
        private readonly Dictionary<Size, HashSet<Shirt>> _sizeSetsDictionary;

        public SearchEngineWithCache(IList<Shirt> shirts)
        {
            _colorSetsDictionary = new Dictionary<Color, HashSet<Shirt>>();
            _sizeSetsDictionary = new Dictionary<Size, HashSet<Shirt>>();
            _shirts = shirts;

            foreach (var color in Color.All)
            {
                _colorSetsDictionary[color] = new HashSet<Shirt>();
            }

            foreach (var size in Size.All)
            {
                _sizeSetsDictionary[size] = new HashSet<Shirt>();
            }

            foreach (var shirt in shirts)
            {
                _colorSetsDictionary[shirt.Color].Add(shirt);
                _sizeSetsDictionary[shirt.Size].Add(shirt);
            }
        }

        public SearchResults Search(SearchOptions options)
        {
            return new SearchResults
            {
                Shirts = GetMatchingShirts(options),
                SizeCounts = GetSizeCounts(),
                ColorCounts = GetColorCounts()
            };
        }

        private IList<Shirt> GetMatchingShirts(SearchOptions options)
        {
            var unionedSizeSets = new HashSet<Shirt>();
            foreach (var size in options.Sizes)
            {
                var sizeSet = _sizeSetsDictionary[size];
                unionedSizeSets.UnionWith(sizeSet);
            }

            var unionedColorSets = new HashSet<Shirt>();
            foreach (var color in options.Colors)
            {
                var colorSet = _colorSetsDictionary[color];
                unionedColorSets.UnionWith(colorSet);
            }

            var finalSet = unionedColorSets.Intersect(unionedSizeSets);
            var matchingShirts = new List<Shirt>(finalSet);

            return matchingShirts;
        }

        private IList<SizeCount> GetSizeCounts()
        {
            var sizeCounts = new List<SizeCount>();

            foreach (var size in _sizeSetsDictionary.Keys)
            {
                var actualCount = _sizeSetsDictionary[size].Count;
                var sizeCount = new SizeCount { Count = actualCount, Size = size };
                sizeCounts.Add(sizeCount);
            }

            return sizeCounts;
        }

        private IList<ColorCount> GetColorCounts()
        {
            var colorCounts = new List<ColorCount>();

            foreach (var color in _colorSetsDictionary.Keys)
            {
                var actualCount = _colorSetsDictionary[color].Count;
                var colorCount = new ColorCount { Count = actualCount, Color = color };
                colorCounts.Add(colorCount);
            }

            return colorCounts;
        }
    }
}