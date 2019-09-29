using System;
using System.Collections.Generic;
using System.Text;
using CSInn.Infrastructure.Repositories.Extensions;
using CSInn.Infrastructure.Repositories.Repositories.CSInn.Experimental.EF.Repositories;
using CSInn.Models;
using Microsoft.EntityFrameworkCore;

namespace CSInn.Infrastructure.Repositories.Tests.Input
{
    public class LessonsRepositoryFixture
    {
        public LessonsRepository Repo { get; set; }
        public LessonsRepositoryFixture()
        {
            var options = new DbContextOptionsBuilder<CSInnDbContext>()
                .UseInMemoryDatabase(databaseName: "database_name")
                .Options;

            var context = new CSInnDbContext(options);

            context.Lessons.Add(
                new Lesson("Lesson 1: Class", "What is a class, object", new List<string>() { "OOP" },
                    new List<string>() { "Almantas Karpavičius" }).ToEntity());

            context.Lessons.Add(new Lesson("Lesson 2: SOLID", "SOLID", new List<string>() { "OOP" },
                new List<string>() { "Kaisinel" }).ToEntity());

            context.Lessons.Add(
                new Lesson("Lesson 3: Delegates", "Delegate vs callback", new List<string>() { "FP" },
                    new List<string>() { "Lethern" }).ToEntity());
            context.SaveChanges();

            Repo = new LessonsRepository(context);
        }
    }
}
