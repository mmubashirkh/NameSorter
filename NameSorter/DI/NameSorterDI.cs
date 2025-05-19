using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NameSorter.Core.Interfaces;
using NameSorter.Core.Services;

namespace NameSorter.DI
{
    public static class NameSorterDI
    {
        public static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddConsole(); 
            });

            services.AddSingleton<IFileHandler, FileHandler>();
            services.AddSingleton<INameSplitter, NameSplitter>();
            services.AddSingleton<ISortNamesAlphabetically, SortNamesAlphabetically>();
            services.AddTransient<NameSorterApp>();

            return services.BuildServiceProvider();
        }

    }
}
