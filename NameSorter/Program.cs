using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NameSorter.DI;

namespace NameSorter;
public class Program
{
    static void Main(string[] args)
    {
        using (ServiceProvider serviceProvider = NameSorterDI.ConfigureServices())
        {
            ILogger<Program> logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            try
            {
                string inputFilePath = args[0];

                NameSorterApp app = serviceProvider.GetRequiredService<NameSorterApp>();
                app.Run(inputFilePath);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred: {Message}", ex.Message);
            }
        }


        //using var loggerFactory = LoggerFactory.Create(builder =>
        //{
        //    builder.AddConsole();
        //});

        //ILogger _logger = loggerFactory.CreateLogger<Program>();

        //IFileHandler fileHandler = new FileHandler(loggerFactory.CreateLogger<FileHandler>());
        //INameSplitter nameSplitter = new NameSplitter(loggerFactory.CreateLogger<NameSplitter>());
        //ISortNamesAlphabetically nameSorter = new SortNamesAlphabetically();

        //try
        //{
        //    if (args.Length != 1)
        //    {
        //        Console.WriteLine("Usage: name-sorter <input-file-path>");
        //        return;
        //    }

        //    string inputPath = args[0];
        //    string outputFilePath = Path.Combine(Directory.GetCurrentDirectory(), "sorted-names-list.txt");

        //    IEnumerable<string> names = fileHandler.ReadNamesFromFile(inputPath);

        //    var splitNames = nameSplitter.SplitName(names);
        //    List<string> sortedNames = nameSorter.SortNames(splitNames);

        //    foreach (var name in sortedNames)
        //    {
        //        Console.WriteLine(name.ToString());
        //    }

        //    fileHandler.WriteNamesToFile(outputFilePath, sortedNames.Select(n => n.ToString()));
        //}
        //catch (Exception ex)
        //{
        //    Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        //}
    }
}
