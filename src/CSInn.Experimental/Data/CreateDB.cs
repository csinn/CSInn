using CSInn.Experimental.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSInn.Experimental.Data
{
    public static class CreateDB
    {
        public static IHost CreateDbIfNotExists(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ExperimentContext>();
                    ExperimentInitializer(context);
                }
                catch (Exception ex)
                {
                    //var logger = services.GetRequiredService<ILogger<Program>>();
                    //logger.LogError(ex, "An error occurred creating the DB.");
                    throw;
                }
            }
            return host;
        }

        public static void ExperimentInitializer(ExperimentContext context)
        {
            context.Database.Migrate();

            // has data? no need to init
            if (context.ExperimentModel.Any())
                return;

            var entries = new ExperimentModel[]
            {
                new ExperimentModel { Title = "Test title 1" },
                new ExperimentModel { Title = "Test title 2" }
            };

            SaveEntries(entries, context, (c) => c.ExperimentModel);
        }

        public static void SaveEntries<T, T2>(T[] entries, T2 context, Func<T2, DbSet<T>>GetModel) where T : class where T2 : DbContext
        {
            DbSet<T> model = GetModel(context);
            foreach (T s in entries)
            {
                model.Add(s);
            }
            context.SaveChanges();
        }
    }
}
