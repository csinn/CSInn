using CSInn.Infrastructure.Repositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSInn.Infrastructure.Repositories.Context
{
    public class CSInnDbContext : DbContext
    {
        public CSInnDbContext()
        {
        }

        public CSInnDbContext(DbContextOptions<CSInnDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<LessonEntity> Lessons { get; set; }
    }
}
