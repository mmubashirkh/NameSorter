using NameSorter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NameSorter.Interfaces
{
    public interface ISortNamesAlphabetically
    {
        List<string> SortNames(IEnumerable<Name> names);
    }
}
