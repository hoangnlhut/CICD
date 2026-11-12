using DemoCICD.Contract.Enumerations;
using DemoCICD.Contract.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCICD.Architecture.Tests
{
    public class SortOrderExtensionTests
    {
        [Fact]
        public void ConvertStringToSortOrderV2_WithNullInput_ReturnsEmptyDictionary()
        {
            // Arrange
            string? input = null;

            // Act
            var result = SortOrderExtension.ConvertStringToSortOrderV2(input);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void ConvertStringToSortOrderV2_WithEmptyString_ReturnsEmptyDictionary()
        {
            // Arrange
            string input = string.Empty;

            // Act
            var result = SortOrderExtension.ConvertStringToSortOrderV2(input);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void ConvertStringToSortOrderV2_WithWhitespaceOnly_ReturnsEmptyDictionary()
        {
            // Arrange
            string input = "   ";

            // Act
            //var result = SortOrderExtension.ConvertStringToSortOrderV2(input);

            // Assert
            Assert.Throws<FormatException>(() => SortOrderExtension.ConvertStringToSortOrderV2(input));
        }

        [Fact]
        public void ConvertStringToSortOrderV2_WithValidSingleColumnASC_ReturnsCorrectDictionary()
        {
            // Arrange
            string input = "Name-ASC";

            // Act
            var result = SortOrderExtension.ConvertStringToSortOrderV2(input);

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, kvp => kvp.Value == SortOrder.Ascending && kvp.Key == "Name");
        }

        [Fact]
        public void ConvertStringToSortOrderV2_WithValidSingleColumnDESC_ReturnsCorrectDictionary()
        {
            // Arrange
            string input = "Price-DESC";

            // Act
            var result = SortOrderExtension.ConvertStringToSortOrderV2(input);

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(result, kvp => kvp.Value == SortOrder.Descending);
        }

        [Fact]
        public void ConvertStringToSortOrderV2_WithMultipleColumns_ReturnsCorrectDictionary()
        {
            // Arrange
            string input = "Name-ASC,Price-DESC,CreatedDate-ASC";

            // Act
            var result = SortOrderExtension.ConvertStringToSortOrderV2(input);

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void ConvertStringToSortOrderV2_WithWhitespaceAroundValues_ReturnsCorrectDictionary()
        {
            // Arrange
            string input = " Name - ASC , Price - DESC ";

            // Act
            var result = SortOrderExtension.ConvertStringToSortOrderV2(input);

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public void ConvertStringToSortOrderV2_WithMissingHyphen_ThrowsFormatException()
        {
            // Arrange
            string input = "NameASC";

            // Act & Assert
            Assert.Throws<FormatException>(() =>
                SortOrderExtension.ConvertStringToSortOrderV2(input));
        }

        [Fact]
        public void ConvertStringToSortOrderV2_WithMissingHyphenInMultiple_ThrowsFormatException()
        {
            // Arrange
            string input = "Name-ASC,PriceDESC";

            // Act & Assert
            Assert.Throws<FormatException>(() =>
                SortOrderExtension.ConvertStringToSortOrderV2(input));
        }

        [Fact]
        public void ConvertStringToSortOrderV2_WithInvalidSeparator_ThrowsFormatException()
        {
            // Arrange
            string input = "Name;ASC;Price;DESC";

            // Act & Assert
            Assert.Throws<FormatException>(() =>
                SortOrderExtension.ConvertStringToSortOrderV2(input));
        }
    }
}
