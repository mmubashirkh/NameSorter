using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Core.Interfaces
{
    public interface IFileHandler
    {
        IEnumerable<string>? ReadNamesFromFile(string filePath);
        bool WriteNamesToFile(string filePath, IEnumerable<string> names);
    }
}
