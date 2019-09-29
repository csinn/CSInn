using CSInn.Domain.Repositories.Specifications.Lesson;
using CSInn.Models;

namespace CSInn.Domain.Repositories.Repositories
{
    public interface ILessonsRepository : IRepository<Lesson, ILessonSpecificationVisitor>,
        IRepositoryAsync<Lesson, ILessonSpecificationVisitor>
    {
        //// TODO: Specification pattern so we can mix criterias.
        //IEnumerable<Lesson> GetByTags(params string[] tags);
        //IEnumerable<Lesson> GetByName(string name);
        //IEnumerable<Lesson> GetByAuthors(params string[] authors);
        //IEnumerable<Lesson> GetByDate(DateTime from, DateTime to);
        //IEnumerable<Lesson> Get(Specification<Lesson> specification);
    }
}
