using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SortBDay
{
    public interface IPersonRepository
    {
        IEnumerable<Person> Get();
    }
}
