using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    public class SearchEngineTestsBase
    {
        private static bool IsMatch(Shirt shirt, List<Guid> filteringSizeIds, List<Guid> filteringColorIds)
        {
            var hasMatchingSize = filteringSizeIds.Contains(shirt.Size.Id);
            var hasMatchingColor = filteringColorIds.Contains(shirt.Color.Id);
            var isMatch = hasMatchingSize && hasMatchingColor;

            return isMatch;
        }

        protected static ISearchEngine GenerateSearchEngine(IList<Shirt> shirts)
        {
            return new SearchEngineWithCache(shirts);
        }

        protected static void AssertSearchDidNotMissShirts(IList<Shirt> allShirts, SearchOptions options, IList<Shirt> foundShirts)
        {
            Assert.That(allShirts, Is.Not.Null);

            var foundShirtIds = foundShirts.Select(s => s.Id).ToList();
            var filteringSizeIds = options.Sizes.Select(s => s.Id).ToList();
            var filteringColorIds = options.Colors.Select(c => c.Id).ToList();

            foreach (var shirt in allShirts)
            {
                var isMatch = IsMatch(shirt, filteringSizeIds, filteringColorIds);
                var isFoundInResults = foundShirtIds.Contains(shirt.Id);

                if (isMatch && !isFoundInResults)
                {
                    Assert.Fail($"'{shirt.Name}' with Size '{shirt.Size.Name}' and Color '{shirt.Color.Name}' not found in results, " +
                                $"when selected sizes where '{string.Join(",", options.Sizes.Select(s => s.Name))}' " +
                                $"and colors '{string.Join(",", options.Colors.Select(c => c.Name))}'");
                }
            }
        }

        protected static void AssertSearchFoundCorrectShirts(IList<Shirt> allShirts, SearchOptions options, IList<Shirt> foundShirts)
        {
            Assert.That(allShirts, Is.Not.Null);

            var foundShirtIds = foundShirts.Select(s => s.Id).ToList();
            var filteringSizeIds = options.Sizes.Select(s => s.Id).ToList();
            var filteringColorIds = options.Colors.Select(c => c.Id).ToList();

            foreach (var shirt in allShirts)
            {
                var isMatch = IsMatch(shirt, filteringSizeIds, filteringColorIds);
                var isFoundInResults = foundShirtIds.Contains(shirt.Id);

                if (!isMatch && isFoundInResults)
                {
                    Assert.Fail($"'{shirt.Name}' with Size '{shirt.Size.Name}' and Color '{shirt.Color.Name}' found in results, " +
                                $"when selected sizes where '{string.Join(",", options.Sizes.Select(s => s.Name))}' " +
                                $"and colors '{string.Join(",", options.Colors.Select(c => c.Name))}'");
                }
            }
        }

        protected static void AssertSizeCounts(IList<Shirt> allShirts, SearchOptions searchOptions, IList<SizeCount> sizeCounts)
        {
            Assert.That(sizeCounts, Is.Not.Null);

            foreach (var size in Size.All)
            {
                var sizeCount = sizeCounts.SingleOrDefault(s => s.Size.Id == size.Id);
                Assert.That(sizeCount, Is.Not.Null, $"Size count for '{size.Name}' not found in results");

                var expectedSizeCount = allShirts.Count(s => s.Size.Id == size.Id);

                Assert.That(sizeCount.Count, Is.EqualTo(expectedSizeCount),
                    $"Size count for '{sizeCount.Size.Name}' showing '{sizeCount.Count}' should be '{expectedSizeCount}'");
            }
        }

        protected static void AssertColorCounts(IList<Shirt> allShirts, SearchOptions searchOptions, IList<ColorCount> colorCounts)
        {
            Assert.That(colorCounts, Is.Not.Null);

            foreach (var color in Color.All)
            {
                var colorCount = colorCounts.SingleOrDefault(s => s.Color.Id == color.Id);
                Assert.That(colorCount, Is.Not.Null, $"Color count for '{color.Name}' not found in results");

                var expectedColorCount = allShirts.Count(c => c.Color.Id == color.Id);

                Assert.That(colorCount.Count, Is.EqualTo(expectedColorCount),
                    $"Color count for '{colorCount.Color.Name}' showing '{colorCount.Count}' should be '{expectedColorCount}'");
            }
        }
    }
}