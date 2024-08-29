using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOK
{
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public IEnumerable<UserDetais> UserDetais { get; set; }
    }
}