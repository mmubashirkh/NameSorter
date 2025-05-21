using Microsoft.Extensions.Logging;
using NameSorter.Interfaces;

namespace NameSorter
{
    public class NameSortProcessor
    {
        private readonly IFileHandler _fileHandler;
        private readonly INameSplitter _nameSplitter;
        private readonly ISortNamesAlphabetically _nameSorter;
        public NameSortProcessor(
            IFileHandler fileHandler,
            INameSplitter nameSplitter,
            ISortNamesAlphabetically nameSorter
            )
        {
            _fileHandler = fileHandler;
            _nameSplitter = nameSplitter;
            _nameSorter = nameSorter;
        }

        public void SortAndWriteNames(string inputFilePath, string outputFilePath)
        {
            try
            {

                var names = _fileHandler.ReadNamesFromFile(inputFilePath);
                if (names == null || !names.Any())
                {
                    Console.WriteLine("No names found in the input file.");
                    return;
                }

                var splitNames = _nameSplitter.SplitName(names);
                if (splitNames == null || splitNames.Count == 0)
                {
                    Console.WriteLine("Failed to split names.");
                    return;
                }

                var sortedNames = _nameSorter.SortNames(splitNames);
                if (sortedNames == null || sortedNames.Count == 0)
                {
                    Console.WriteLine("Failed to sort names.");
                    return;
                }

                Console.WriteLine("-- Sorted Names --");
                foreach (var name in sortedNames)
                {
                    Console.WriteLine(name);
                }
                Console.WriteLine("------------------");

                Console.WriteLine($"Writing sorted names to: {outputFilePath}");
                if (_fileHandler.WriteNamesToFile(outputFilePath, sortedNames))
                {
                    Console.WriteLine("Sorted names written to file successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to write sorted names to file.");
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
