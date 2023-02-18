using Microsoft.EntityFrameworkCore;
using PersonTablesDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonTablesDataAccess.Data
{
    public class PeopleContext : DbContext
    {
        public DbSet<Person> People { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConfigurationManager.ConnectionStrings["data"].ConnectionString);
        }
    }
}
