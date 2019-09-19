using System;
using System.Collections.Generic;
using System.Linq;
using CSInn.Experimental.EF.Data;
using CSInn.Experimental.EF.Extensions;
using CSInn.Experimental.Entities;
using CSInn.Models;
using Microsoft.EntityFrameworkCore;

namespace CSInn.Experimental.EF.Repositories
{
    public class ExperimantalRepository
    {
        private readonly ExperimentContext _context;
        private DbSet<ExperimentEntity> dbSet => _context.ExperimentEntities;

        public ExperimantalRepository(ExperimentContext context)
        {
            _context = context;
        }

        public IEnumerable<Experiment> Get() => dbSet.ToModels();
        public IEnumerable<Experiment> Get(string title) => dbSet.Where(e => e.Title == title).Select(e => e.ToModel());
        public void Create(Experiment model) => dbSet.Add(model.ToEntity());

        public void Update(Experiment model)
        {
            var entity = dbSet.Find(model.Id);
              
            if (entity == null)
            {
                return;
            }

            _context.Entry(entity).CurrentValues.SetValues(model);
        }

        public void Delete(Experiment model)
        {
            var entity = dbSet.Find(model.Id);
            dbSet.Remove(entity);
        }


    }
}
