using System;
using System.Collections.Generic;
using CSInn.Models;

namespace CSInn.Domain.Repositories
{
    public interface ILessonsRepository : IRepository<Lesson>
    {
        // TODO: Specification pattern so we can mix criterias.
        IEnumerable<Lesson> GetByTags(params string[] tags);
        IEnumerable<Lesson> GetByName(string name);
        IEnumerable<Lesson> GetByAuthors(params string[] authors);
        IEnumerable<Lesson> GetByDate(DateTime from, DateTime to);
    }
}
