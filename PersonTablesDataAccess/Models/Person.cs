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
        public string FullName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = null!;
    }
}
