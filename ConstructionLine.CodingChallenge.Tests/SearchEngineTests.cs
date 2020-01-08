using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        private IList<Shirt> _shirts;
        private SearchOptions _searchOptions;

        [SetUp]
        public void InitializeShirts()
        {
            _shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            _searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };
        }

        [Test]
        public void ShouldReturnOnlyCorrectShirts()
        {
            // Arrange
            var searchEngine = GenerateSearchEngine(_shirts);

            // Act
            var results = searchEngine.Search(_searchOptions);

            // Assert
            AssertSearchFoundCorrectShirts(_shirts, _searchOptions, results.Shirts);
        }

        [Test]
        public void ShouldReturnAllMatchingShirts()
        {
            // Arrange
            var searchEngine = GenerateSearchEngine(_shirts);

            // Act
            var results = searchEngine.Search(_searchOptions);

            // Assert
            AssertSearchDidNotMissShirts(_shirts, _searchOptions, results.Shirts);
        }

        [Test]
        public void ShouldReturnCorrectSizeCounts()
        {
            // Arrange
            var searchEngine = GenerateSearchEngine(_shirts);

            // Act
            var results = searchEngine.Search(_searchOptions);

            // Assert
            AssertSizeCounts(_shirts, _searchOptions, results.SizeCounts);
        }

        [Test]
        public void ShouldReturnCorrectColorCounts()
        {
            // Arrange
            var searchEngine = GenerateSearchEngine(_shirts);

            // Act
            var results = searchEngine.Search(_searchOptions);

            // Assert
            AssertColorCounts(_shirts, _searchOptions, results.ColorCounts);
        }
    }
}