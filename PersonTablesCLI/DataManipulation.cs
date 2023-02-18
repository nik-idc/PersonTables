using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using PersonTablesDataAccess.Data;
using PersonTablesDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.Security.Cryptography;
using System.Net.Mime;

namespace PersonTablesCLI
{
    public static class DataManipulation
    {
        private static readonly string _rules = "Action codes from 1 to 6 inclusively are allowed.\n" +
                        "- '1' creates a table (if one didn't exist prior);\n" +
                        "- '2' creates person with specified arguments, arguments should be (Name BirthDate Gender);\n" +
                        "- '3' shows sorted people with distinct names and birth dates;\n" +
                        "- '4' creates a million random records in the database;\n" +
                        "- '5' shows only men with full name starting with 'F';\n" +
                        "- '6' does what '5' does, but faster;\n";

        private static void CreateTable()
        {
            // Creating a new context instance will create a database
            // if one does not exist

            using PeopleContext context = new();
            context.Database.Migrate();
        }

        private static void CreatePerson(string[] args)
        {
            using PeopleContext context = new();

            // Create person to add based on the arguments format
            Person newPerson = new()
            {
                FullName = args[2],
                BirthDate = DateTime.Parse(args[3]),
                Gender = args[4],
            };

            // Adding new person to the table and saving changes
            context.Add(newPerson);
            context.SaveChanges();
        }

        private static void ShowSortedPeople()
        {
            using PeopleContext context = new();

            // Sort people
            var sortedPeople = context.People.
                OrderBy(p => p.FullName).
                Select(p => new { p.FullName, p.BirthDate, p.Gender, Age = (DateTime.Now - p.BirthDate).Days / 365 }).
                GroupBy(p => new { p.FullName, p.BirthDate }).ToList();

            // Display the result
            Console.WriteLine("Sorted people:");
            foreach (var peopleGroup in sortedPeople)
            {
                foreach (var person in peopleGroup)
                {
                    Console.WriteLine($"\tName: {person.FullName}, Birth Date: {person.BirthDate}, Gender: {person.Gender}, Age: {person.Age}");
                }
            }
        }

        private static void CreateMillionRecords()
        {
            using PeopleContext context = new();

            // Create a million random users with an approximately equal gender distribution
            Random rng = new();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // All available letters
            DateTime maxAge = new(1950, 1, 1); // Maximum age
            DateTime minAge = new(2023, 2, 18); // Minimum age
            int daysRange = (minAge - maxAge).Days; // Range between the max and min ages
            for (int i = 0; i < 1000000; i++)
            {
                // Create random person and add them to the table
                Person newPerson = new()
                {
                    FullName = $"{chars[rng.Next(chars.Length)]}{chars[rng.Next(chars.Length)]}{chars[rng.Next(chars.Length)]}",
                    BirthDate = maxAge.AddDays(rng.Next(daysRange)),
                    Gender = rng.NextDouble() > 0.5 ? "Male" : "Female",
                };
                context.Add(newPerson);
            }

            context.SaveChanges();
        }

        private static void ShowMen()
        {
            // Create and start stopwatch
            Stopwatch sw = new();
            sw.Start();

            // Query
            using PeopleContext context = new();

            var selectedMen = context.People.Where(p => p.FullName.StartsWith("f") && p.Gender == "Male").ToList();

            // Stop stopwatch and print elapsed time
            sw.Stop();
            Console.WriteLine($"Men selection complete.\nRows selected: {selectedMen.Count}\nElapse time: {sw.Elapsed}");
        }

        private static void ShowMenFaster()
        {
            // TODO: Make it faster

            // Create and start stopwatch
            Stopwatch sw = new();
            sw.Start();

            // Query
            using PeopleContext context = new();

            var selectedMen = context.People.Where(p => p.FullName.StartsWith("f") && p.Gender == "Male").ToList();

            // Stop stopwatch and print elapsed time
            sw.Stop();
            Console.WriteLine($"Men selection complete.\nRows selected: {selectedMen.Count}\nElapse time: {sw.Elapsed}");
        }

        public static void PerformAction(string[] args)
        {
            int actionCode = int.Parse(args[1]);

            switch (actionCode)
            {
                case 1:
                    CreateTable();
                    break;
                case 2:
                    CreatePerson(args);
                    break;
                case 3:
                    ShowSortedPeople();
                    break;
                case 4:
                    CreateMillionRecords();
                    break;
                case 5:
                    ShowMen();
                    break;
                case 6:
                    ShowMenFaster();
                    break;
                default:
                    throw new ArgumentException($"Can't parse action code.\n{_rules}");
            }
        }
    }
}
