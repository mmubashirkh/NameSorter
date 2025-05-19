using Microsoft.Extensions.Logging;
using NameSorter.Core.Interfaces;
using NameSorter.Core.Models;

namespace NameSorter.Core.Services
{
    public class SortNamesAlphabetically : ISortNamesAlphabetically
    {
        public List<string> SortNames(IEnumerable<Name> nameParsedList)
        {
            return nameParsedList
                .OrderBy(n => n.LastName)
                .ThenBy(n => string.Join(" ", n.GivenNames))
                .Select(n => n.ToString())
                .ToList();
        }
    }
}
