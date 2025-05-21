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
                string outputFilePath = Path.Combine(Directory.GetCurrentDirectory(), "sorted-names-list.txt");

                NameSortProcessor nameSortProcess = serviceProvider.GetRequiredService<NameSortProcessor>();
                nameSortProcess.SortAndWriteNames(inputFilePath, outputFilePath);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred: {Message}", ex.Message);
            }
        }
    }
}
