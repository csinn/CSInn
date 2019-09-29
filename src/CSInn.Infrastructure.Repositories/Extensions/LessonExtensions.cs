using System.Collections.Generic;
using System.Linq;
using CSInn.Infrastructure.Repositories.Entities;
using CSInn.Models;

namespace CSInn.Infrastructure.Repositories.Extensions
{
    public static class LessonExtensions
    {
        public static Lesson ToModel(this LessonEntity entity)
        {
            // call to automapper
            var lesson = new Lesson(entity.Title, entity.Description, entity.Tags.Split(","), entity.Authors.Split(","))
            {
                Id = entity.Id,
            };

            return lesson;
        }

        public static IEnumerable<Lesson> ToModels(this IEnumerable<LessonEntity> entities)
        {
            // call to automapper
            var experiments = entities.Select(e => e.ToModel());

            return experiments;
        }

        public static LessonEntity ToEntity(this Lesson entity)
        {
            // call to automapper
            var experiment = new LessonEntity()
            {
                Id = entity.Id,
                Title = entity.Title,
                Authors = string.Join(',', entity.Authors),
                Description = entity.Description,
                Slides = entity.Slides,
                Tags = string.Join(',',entity.Tags)
            };

            return experiment;
        }
    }
}
