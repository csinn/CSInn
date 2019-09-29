using CSInn.Domain.Repositories.Repositories;
using CSInn.Domain.Repositories.Specifications.Lesson;
using CSInn.Infrastructure.Repositories.Context;
using CSInn.Infrastructure.Repositories.Entities;
using CSInn.Infrastructure.Repositories.Expressions;
using CSInn.Models;

namespace CSInn.Infrastructure.Repositories.Repositories
{
    namespace CSInn.Experimental.EF.Repositories
    {
        public class LessonsRepository : Repository<Lesson, ILessonSpecificationVisitor, LessonEntityExpressionVisitor, LessonEntity>,
            ILessonsRepository
        {
            public LessonsRepository(CSInnDbContext context) : base(context)
            {
            }

            protected override Lesson ToModel(LessonEntity entity)
            {
                // call to automapper
                var lesson = new Lesson(entity.Title, entity.Description, entity.Tags.Split(","), entity.Authors.Split(","))
                {
                    Id = entity.Id,
                };

                return lesson;
            }

            protected override LessonEntity ToEntity(Lesson model)
            {
                // call to automapper
                var lesson = new LessonEntity()
                {
                    Id = model.Id,
                    Title = model.Title,
                    Authors = string.Join(',', model.Authors),
                    Description = model.Description,
                    Slides = model.Slides,
                    Tags = string.Join(',', model.Tags)
                };

                return lesson;
            }

            protected override int ExtractKey(Lesson model) => model.Id;
        }
    }
}

