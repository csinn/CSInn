using System.Collections.Generic;
using CSInn.Infrastructure.Repositories.Context;
using CSInn.Infrastructure.Repositories.UoW;
using CSInn.Models;
using Microsoft.EntityFrameworkCore;

namespace CSInn.Infrastructure.Repositories.Tests.Input
{
    public class UoWFixture
    {
        public CSInnUnitOfWork UoW { get; set; }
        public UoWFixture()
        {
            var options = new DbContextOptionsBuilder<CSInnDbContext>()
                .UseInMemoryDatabase(databaseName: "database_name")
                .Options;

            var context = new CSInnDbContext(options);
            UoW = new CSInnUnitOfWork(context);

            UoW.Lessons.Create(new Lesson("Lesson 2: SOLID", "SOLID", new List<string>() { "OOP" },
                new List<string>() { "Kaisinel" }));

            UoW.Lessons.Create(
                new Lesson("Lesson 3: Delegates", "Delegate vs callback", new List<string>() { "FP" },
                    new List<string>() { "Lethern" }));

            UoW.Save();
        }
    }
}
