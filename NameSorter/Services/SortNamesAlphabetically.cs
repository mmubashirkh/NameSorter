using Microsoft.Extensions.Logging;
using NameSorter.Interfaces;
using NameSorter.Models;

namespace NameSorter.Services
{
    public class SortNamesAlphabetically : ISortNamesAlphabetically
    {
        public List<string> SortNames(IEnumerable<Name> nameParsedList)
        {
            return nameParsedList
                .OrderBy(x => x.LastName)
                .ThenBy(x => string.Join(" ", x.GivenNames))
                .Select(x => x.ToString())
                .ToList();
        }
    }
}
