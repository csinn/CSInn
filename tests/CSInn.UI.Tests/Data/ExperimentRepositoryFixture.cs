using CSInn.Experimental.EF.Data;
using CSInn.Experimental.EF.Repositories;
using CSInn.Experimental.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSInn.UI.Tests.Data
{
    public class ExperimentRepositoryFixture
    {
        public ExperimantalRepository Repo { get; set; }
        public ExperimentRepositoryFixture()
        {
            var options = new DbContextOptionsBuilder<ExperimentContext>()
                .UseInMemoryDatabase(databaseName: "database_name")
                .Options;


            var context = new ExperimentContext(options);
            context.ExperimentEntities.Add(
                    new ExperimentEntity
                    {
                        Id = 1,
                        Title = "Hello world"
                    });
                context.ExperimentEntities.Add(
                    new ExperimentEntity
                    {
                        Id = 2,
                        Title = "Hey"
                    });
                context.SaveChanges();

            Repo = new ExperimantalRepository(context);
        }
    }
}
