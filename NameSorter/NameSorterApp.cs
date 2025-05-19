using Microsoft.Extensions.Logging;
using NameSorter.Core.Interfaces;

namespace NameSorter
{
    public class NameSorterApp
    {
        private readonly IFileHandler _fileHandler;
        private readonly INameSplitter _nameSplitter;
        private readonly ISortNamesAlphabetically _nameSorter;
        private readonly ILogger<NameSorterApp> _logger;
        public NameSorterApp(
            IFileHandler fileHandler,
            INameSplitter nameSplitter,
            ISortNamesAlphabetically nameSorter,
            ILogger<NameSorterApp> logger)
        {
            _fileHandler = fileHandler;
            _nameSplitter = nameSplitter;
            _nameSorter = nameSorter;
            _logger = logger;
        }

        public void Run(string filePath)
        {
            string outputFilePath = Path.Combine(Directory.GetCurrentDirectory(), "sorted-names-list.txt");

            try
            {
                var names = _fileHandler.ReadNamesFromFile(filePath);
                if (names == null || names.Count() == 0)
                {
                    Console.WriteLine("No names found in the input file.");
                    return;
                }

                var splitedNames = _nameSplitter.SplitName(names);
                if (splitedNames == null || splitedNames.Count() == 0)
                {
                    Console.WriteLine("Failed to split names.");
                    return;
                }

                var sortedNames = _nameSorter.SortNames(splitedNames);
                if (sortedNames == null || sortedNames.Count() == 0)
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
                _fileHandler.WriteNamesToFile(outputFilePath, sortedNames);
                Console.WriteLine("Sorted names written to file successfully.");
            }
            catch (FileNotFoundException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
