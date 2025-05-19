using NameSorter.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Core.Interfaces
{
    public interface INameSplitter
    {
        List<Name> SplitName(IEnumerable<string> names);
    }
}
