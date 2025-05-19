using NameSorter.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;

namespace NameSorter.Core.Services
{
    public class FileHandler : IFileHandler
    {
        private readonly ILogger _logger;
        public FileHandler(ILogger<FileHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public IEnumerable<string>? ReadNamesFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                _logger.LogError("File not found: {Path}", filePath);
                throw new FileNotFoundException("Input file not found.", filePath);
            }

            var lines = File.ReadAllLines(filePath);//.Where(line => !string.IsNullOrWhiteSpace(line)).ToList();

            if (!lines.Any())
            {
                _logger.LogWarning("Input file is empty: {Path}", filePath);
                return new List<string>();
            }

            return lines;
        }

        public bool WriteNamesToFile(string filePath, IEnumerable<string> names)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    _logger.LogError("FileWriter: File path is null or empty.");
                    return false;
                }

                if (names == null)
                {
                    _logger.LogError("FileWriter: Names collection is null.");
                    return false;
                }

                File.WriteAllLines(filePath, names);
                return true;
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "FileWriter: Error writing to file: {FilePath}", filePath);
                return false;
            }
        }
    }
}
