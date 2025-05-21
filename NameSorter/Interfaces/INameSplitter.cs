using NameSorter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Interfaces
{
    public interface INameSplitter
    {
        List<Name> SplitName(IEnumerable<string> names);
    }
}
