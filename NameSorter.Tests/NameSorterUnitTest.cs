using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Logging;
using Moq;
using NameSorter.Interfaces;
using NameSorter.Models;
using NameSorter.Services;
using System;

namespace NameSorter.Tests
{
    public class FileHandlerUnitTest
    {
        private readonly Mock<ILogger<FileHandler>> _mockLogger = new();
        private readonly FileHandler _fileHandler = new FileHandler(new Mock<ILogger<FileHandler>>().Object);

        [Fact]
        public void WriteNamesToFile_ReturnsFalse_WhenFilePathIsNullOrWhiteSpace()
        {
            var result = _fileHandler.WriteNamesToFile(null!, new List<string> { "John Doe" });
            Assert.False(result);

        }

        [Fact]
        public void WriteNamesToFile_ReturnsTrue_AndWritesFile_WhenValidInput()
        {
            var tempFile = Path.GetTempFileName();
            try
            {
                var names = new List<string> { "Janet Parsons", "Vaughn Lewis" };
                var result = _fileHandler.WriteNamesToFile(tempFile, names);
                Assert.True(result);
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }
    }
    public class NameSplitterUnitTest
    {
        private readonly Mock<ILogger<NameSplitter>> _mockLogger = new();
        private readonly NameSplitter _nameSplitter = new NameSplitter(new Mock<ILogger<NameSplitter>>().Object);

        [Fact]
        public void SplitName_ValidNames_ReturnsParsedNames()
        {
            var input = new[] { "Leo Gardner", "Beau Tristan Bentley", "Hunter Uriah Mathew Clarke" };

            var result = _nameSplitter.SplitName(input);

            Assert.NotNull(result);
            Assert.Equal(3, result.Count);

            Assert.Equal(new[] { "Leo" }, result[0].GivenNames);
            Assert.Equal("Gardner", result[0].LastName);

            Assert.Equal(new[] { "Beau", "Tristan" }, result[1].GivenNames);
            Assert.Equal("Bentley", result[1].LastName);

            Assert.Equal(new[] { "Hunter", "Uriah", "Mathew" }, result[2].GivenNames);
            Assert.Equal("Clarke", result[2].LastName);
        }

        [Fact]
        public void SplitName_NameWithLessThanTwoParts_Skipped()
        {
            var input = new List<string> { "Janet" };

            var result = _nameSplitter.SplitName(input);

            Assert.Empty(result);
        }

        [Fact]
        public void SplitName_NameWithMoreThanFourParts_Skipped()
        {

            var input = new List<string> { "Hunter Uriah Mathew Clarke Test" };

            var result = _nameSplitter.SplitName(input);

            Assert.Empty(result);
        }

        [Fact]
        public void SplitName_NullOrWhitespaceName_ReturnsNull()
        {
            var names = new List<string> { "", "   ", null };
            var result = _nameSplitter.SplitName(names);
            Assert.Empty(result);
        }

        [Fact]
        public void SplitName_EmptyInput_ReturnsEmptyList()
        {
            var result = _nameSplitter.SplitName(Array.Empty<string>());

            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
    public class SortNamesAlphabeticallyUnitTest
    {
        [Fact]
        public void SortNames_SortsByLastNameThenGivenNames()
        {
            var sorter = new SortNamesAlphabetically();
            var names = new List<Name>
            {
                new Name { GivenNames = new[] { "Adonis", "Julius" }, LastName = "Archer" },
                new Name { GivenNames = new[] { "Shelby", "Nathan" }, LastName = "Yoder" },
                new Name { GivenNames = new[] { "Marin" }, LastName = "Alvarez" },
                new Name { GivenNames = new[] { "London" }, LastName = "Lindsey" }
            };

            var result = sorter.SortNames(names);

            Assert.Equal(new[]
            {
                "Marin Alvarez",
                "Adonis Julius Archer",
                "London Lindsey",
                "Shelby Nathan Yoder"
            }, result);
        }

        [Fact]
        public void SortNames_EmptyInput_ReturnsEmptyList()
        {
            var sorter = new SortNamesAlphabetically();
            var result = sorter.SortNames(new List<Name>());
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void SortNames_SameLastNameDifferentGivenNames_SortsByGivenNames()
        {
            var sorter = new SortNamesAlphabetically();
            var names = new List<Name>
            {
                new Name { GivenNames = new[] { "Leo" }, LastName = "Gardner" },
                new Name { GivenNames = new[] { "Vaughn" }, LastName = "Gardner" }
            };

            var result = sorter.SortNames(names);

            Assert.Equal(new[]
            {
                "Leo Gardner",
                "Vaughn Gardner"
            }, result);
        }
    }
}