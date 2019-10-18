using CSInn.Infrastructure.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program_old
    {
        static void Main_old(string[] args)
        {
            /*
            List<LessonEntity> test_list = new List<LessonEntity>()
            {
                new LessonEntity() { entity_test  = "1"},
                new LessonEntity() { entity_test  = "2"},
                new LessonEntity() { entity_test  = "3"},
                new LessonEntity() { entity_test  = "4"},
                new LessonEntity() { entity_test  = "5"},
            };


            IQueryable<LessonEntity> EntitySet = (from p in test_list select p).AsQueryable();

            var dao = new LessonDao(EntitySet);
            var dto = new LessonDto(dao);

            var result = dto.findByTest("2");
            Console.WriteLine($"cout: {result.Count}");
            Console.WriteLine($"model_test: {result[0].model_test}");
            */
        }
    }
}
