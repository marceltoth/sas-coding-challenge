using System;
using System.Collections.Generic;
using System.Diagnostics;
using ConstructionLine.CodingChallenge.Tests.SampleData;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEnginePerformanceTests : SearchEngineTestsBase
    {
        private List<Shirt> _shirts;
        private ISearchEngine _searchEngine;

        [SetUp]
        public void Setup()
        {
            var dataBuilder = new SampleDataBuilder(50000);
            _shirts = dataBuilder.CreateShirts();
            _searchEngine = GenerateSearchEngine(_shirts);
        }

        [Test]
        public void PerformanceTest()
        {
            // Arrange
            var sw = new Stopwatch();
            sw.Start();

            var options = new SearchOptions
            {
                Colors = new List<Color> { Color.Red }
            };

            // Act
            var results = _searchEngine.Search(options);
            sw.Stop();
            var totalMiliseconds = sw.ElapsedMilliseconds;
            Console.WriteLine($"Test fixture finished in {totalMiliseconds} milliseconds");

            // Assert
            var foundShirts = results.Shirts;
            AssertSearchFoundCorrectShirts(_shirts, options, foundShirts);
            AssertSearchDidNotMissShirts(_shirts, options, foundShirts);

            AssertSizeCounts(_shirts, options, results.SizeCounts);
            AssertColorCounts(_shirts, options, results.ColorCounts);
        }
    }
}