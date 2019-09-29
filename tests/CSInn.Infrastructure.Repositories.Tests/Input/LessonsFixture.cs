using System.Collections.Generic;
using System.Linq;
using CSInn.Models;

namespace CSInn.Infrastructure.Repositories.Tests.Input
{
    public class LessonsFixture
    {
        public readonly IQueryable<Lesson> Lessons;

        public LessonsFixture()
        {
            var lessons = new[]
            {
                new Lesson("Lesson 1: Class", "What is a class, object", new List<string>() {"OOP"},
                    new List<string>() {"Almantas Karpavičius"}),
                new Lesson("Lesson 2: SOLID", "SOLID", new List<string>() {"OOP"}, new List<string>() {"Kaisinel"}),
                new Lesson("Lesson 3: Delegates", "Delegate vs callback", new List<string>() {"FP"},
                    new List<string>() {"Lethern"})
            };
            Lessons = new EnumerableQuery<Lesson>(lessons);
        }
    }
}
