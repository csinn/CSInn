using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CSInn.Experimental.Models
{
    public class ExperimentContext : DbContext
    {
        public ExperimentContext (DbContextOptions<ExperimentContext> options)
            : base(options)
        {
        }

        public DbSet<CSInn.Experimental.Models.ExperimentModel> ExperimentModel { get; set; }
    }
}
