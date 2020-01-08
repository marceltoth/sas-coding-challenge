﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests_Primitives : SearchEngineTestsBase
    {
        [Test]
        public void ShouldReturnCorrectShirt_WhenQueryingSingleSizeWithoutColor()
        {
            // Arrange
            var expectedId = Guid.NewGuid();
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "1. Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "2. Red - Large", Size.Large, Color.Red),
                new Shirt(expectedId, "3. Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "4. Black - Large", Size.Large, Color.Black),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Sizes = new List<Size> { Size.Medium }
            };

            // Act
            var results = searchEngine.Search(searchOptions);

            // Assert
            AssertColorCounts(results.ColorCounts, red: 2, blue: 0, yellow: 0, white: 0, black: 2);
            AssertSizeCounts(results.SizeCounts, small: 1, medium: 1, large: 2);
            AsserResultShirts(results.Shirts, expectedId);
        }

        [Test]
        public void ShouldReturnCorrectShirt_WhenQueryingSingleColorWithoutSize()
        {
            // Arrange
            var expectedId1 = Guid.NewGuid();
            var expectedId2 = Guid.NewGuid();
            var shirts = new List<Shirt>
            {
                new Shirt(expectedId1, "1. Red - Small", Size.Small, Color.Red),
                new Shirt(expectedId2, "2. Red - Large", Size.Large, Color.Red),
                new Shirt(Guid.NewGuid(), "3. Black - Small", Size.Small, Color.Black),
                new Shirt(Guid.NewGuid(), "3. Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "4. Black - Large", Size.Large, Color.Black),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red }
            };

            // Act
            var results = searchEngine.Search(searchOptions);

            // Assert
            AssertColorCounts(results.ColorCounts, red: 2, blue: 0, yellow: 0, white: 0, black: 3);
            AssertSizeCounts(results.SizeCounts, small: 2, medium: 1, large: 2);
            AsserResultShirts(results.Shirts, expectedId1, expectedId2);
        }

        private static void AssertColorCounts(IList<ColorCount> colorCounts,
            int red,
            int blue,
            int yellow,
            int white,
            int black)
        {
            var actualRedCount = colorCounts.SingleOrDefault(cc => cc.Color.Id == Color.Red.Id).Count;
            var actualBlueCount = colorCounts.SingleOrDefault(cc => cc.Color.Id == Color.Blue.Id).Count;
            var actualYellowCount = colorCounts.SingleOrDefault(cc => cc.Color.Id == Color.Yellow.Id).Count;
            var actualWhiteCount = colorCounts.SingleOrDefault(cc => cc.Color.Id == Color.White.Id).Count;
            var actualBlackCount = colorCounts.SingleOrDefault(cc => cc.Color.Id == Color.Black.Id).Count;

            Assert.That(actualRedCount == red);
            Assert.That(actualBlueCount == blue);
            Assert.That(actualYellowCount == yellow);
            Assert.That(actualWhiteCount == white);
            Assert.That(actualBlackCount == black);
        }

        private static void AssertSizeCounts(IList<SizeCount> colorCounts,
            int small,
            int medium,
            int large)
        {
            var actualSmallCount = colorCounts.SingleOrDefault(cc => cc.Size.Id == Size.Small.Id).Count;
            var actualMediumCount = colorCounts.SingleOrDefault(cc => cc.Size.Id == Size.Medium.Id).Count;
            var actualLargeCount = colorCounts.SingleOrDefault(cc => cc.Size.Id == Size.Large.Id).Count;

            Assert.That(actualSmallCount == small);
            Assert.That(actualMediumCount == medium);
            Assert.That(actualLargeCount == large);
        }

        private static void AsserResultShirts(IList<Shirt> foundShirts, params Guid[] expectedIds)
        {
            Assert.NotNull(expectedIds);
            Assert.NotNull(foundShirts);

            Assert.IsTrue(expectedIds.Length == foundShirts.Count);
            foreach (var expectedId in expectedIds)
            {
                var occurrences = foundShirts.Count(shirt => shirt.Id == expectedId);
                Assert.IsTrue(occurrences == 1);
            }
        }
    }
}