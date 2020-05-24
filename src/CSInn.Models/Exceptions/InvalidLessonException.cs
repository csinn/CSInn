// -------------------------------------------------------------------------------------------------
// C# Inn Website - © Copyright 2020 - C# Inn
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace CSInn.Domain.Models.Content.Exceptions
{
    public class InvalidLessonException : Exception
    {
        public InvalidLessonException(bool isTitleEmpty, bool isDescriptionEmpty, bool areNoTags, bool areNoAuthors)
            : base(BuildErrorMessage(isTitleEmpty, isDescriptionEmpty, areNoTags, areNoAuthors))
        {
        }

        private static string BuildErrorMessage(bool isTitleEmpty, bool isDescriptionEmpty, bool areNoTags, bool areNoAuthors)
        {
            var missing = new List<string>();

            if (!isTitleEmpty)
            {
                missing.Add("title");
            }

            if (!isDescriptionEmpty)
            {
                missing.Add("description");
            }

            if (!areNoTags)
            {
                missing.Add("tags");
            }

            if (!areNoAuthors)
            {
                missing.Add("authors");
            }

            var error = $"Lesson is missing: {string.Join(",", missing)}";

            return error;
        }
    }
}
