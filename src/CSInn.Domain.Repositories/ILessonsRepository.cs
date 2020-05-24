// -------------------------------------------------------------------------------------------------
// C# Inn Website - © Copyright 2020 - C# Inn
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using CSInn.Domain.Models.Content;

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
