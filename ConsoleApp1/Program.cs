using CSInn.Infrastructure.Repositories.Context;
using CSInn.Infrastructure.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using CSInn.Infrastructure.Repositories.UoW;
using CSInn.Models;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var options = new DbContextOptionsBuilder<CSInnDbContext>()
                .UseSqlServer(configuration.GetConnectionString("CSINN"))
                .Options;

            //     .UseInMemoryDatabase(databaseName: "database_name")
            //     .Options;
            //

            var context = new CSInnDbContext(options);
            var dbSet = context.Set<LessonEntity>();

            if (dbSet.Any())
                Console.WriteLine("has stuff");
            else
            {
                for (int i = 0; i < 5; ++i)
                    dbSet.Add(new LessonEntity() { entity_test = "" + i });

                context.SaveChanges();
            }

            IQueryable<LessonEntity> EntitySet = dbSet;
            var dao = new LessonDao(EntitySet);
            var dto = new LessonDto(dao);

            var result = dto.findByTest("2", "tmp");

            Console.WriteLine($"cout: {result.Count}");
            foreach(var r in result)
                Console.WriteLine($"model_test: {r.model_test}");
        }
    }
}
