using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSInn.Experimental.Entities;
using CSInn.Models;
using Microsoft.EntityFrameworkCore;

namespace CSInn.Experimental.EF.Extensions
{
    public static class ExperimentExtensions
    {
        public static Experiment ToModel(this ExperimentEntity entity)
        {
            // call to automapper
            var experiment = new Experiment()
            {
                Id = entity.Id,
                Title = entity.Title
            };

            return experiment;
        }

        public static IEnumerable<Experiment> ToModels(this DbSet<ExperimentEntity> entities)
        {
            // call to automapper
            var experiments = entities.Select(e => e.ToModel());

            return experiments;
        }

        public static ExperimentEntity ToEntity(this Experiment entity)
        {
            // call to automapper
            var experiment = new ExperimentEntity()
            {
                Id = entity.Id,
                Title = entity.Title
            };

            return experiment;
        }
    }
}
