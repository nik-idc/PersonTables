using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonTablesDataAccess.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Patronim { get; set; }

        public DateTime BirthdDate { get; set; }

        public string Gender { get; set; } = null!;
    }
}
