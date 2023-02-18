using PersonTablesDataAccess.Data;
using PersonTablesDataAccess.Models;

using PeopleContext context = new PeopleContext();

Person vlad = new Person()
{
    FirstName = "Vlad",
    LastName = "Petrov",
    Gender = "Male",
    BirthdDate = new DateTime(2001, 10, 8, 2, 3, 5),
};

context.Add(vlad);
context.SaveChanges();