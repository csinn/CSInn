using CSInn.Experimental.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSInn.Experimental.EF.Data
{
    public class ExperimentContext : DbContext
    {
        public ExperimentContext()
        {

        }

        public ExperimentContext (DbContextOptions<ExperimentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ExperimentEntity> ExperimentEntities { get; set; }
    }
}
