using Microsoft.Extensions.Logging;
using NameSorter.Interfaces;
using NameSorter.Models;

namespace NameSorter.Services
{
    public class NameSplitter : INameSplitter
    {
        private readonly ILogger<NameSplitter> _logger;

        public NameSplitter(ILogger<NameSplitter> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public List<Name> SplitName(IEnumerable<string> names)
        {
            List<Name> nameParsedList = new List<Name>();

            foreach (var fullName in names)
            {

                if (string.IsNullOrWhiteSpace(fullName))
                {
                    _logger.LogWarning("NameSplitter: Input name is null or empty.");
                     continue;
                }

                string[] fullNameSplit = fullName.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (fullNameSplit.Length < 2)
                {
                    _logger.LogWarning("NameSplitter: Input name has less than two parts: {FullName}", fullName);
                    continue;
                }
                if (fullNameSplit.Length > 4)
                {
                    _logger.LogWarning("NameSplitter: Input name has more than 4 parts: {FullName}", fullName);
                    continue; 
                }

                string lastName = fullNameSplit[fullNameSplit.Length - 1];
                string[] givenNames = fullNameSplit.Take(fullNameSplit.Length - 1).ToArray();

                nameParsedList.Add(new Name
                {
                    GivenNames = givenNames,
                    LastName = lastName
                });
            }
            return nameParsedList;
        }
    }
}
