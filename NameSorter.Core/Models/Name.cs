using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Core.Models
{
    public class Name
    {
        public required string[] GivenNames { get; set; }
        public required string LastName { get; set; }

        public override string ToString()
        {
            return string.Join(" ", GivenNames.Append(LastName));
        }
    }
}
