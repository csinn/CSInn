using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CSInn.Domain.Models.Content.Exceptions;

[assembly: InternalsVisibleTo("CSInn.Infrastructure.Repositories")]
namespace CSInn.Models
{
    public class Lesson
    {
        internal int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Video { get; set; }
        public string Slides { get; set; }
        public IList<string> Tags { get; set; }
        public IList<string> Authors { get; set; }

        public string model_test { get; set; }

        public Lesson(string title, string description, IEnumerable<string> tags, IEnumerable<string> authors)
        {
            Title = title;
            Description = description;
            Tags = tags?.ToList();
            Authors = authors?.ToList();

            var isTitleEmpty = string.IsNullOrEmpty(title);
            var isDescriptionEmpty = string.IsNullOrEmpty(description);
            var areNoTags = Tags == null || !Tags.Any();
            var areNoAuthors = Authors == null || !Authors.Any();

            if (isTitleEmpty || isDescriptionEmpty || areNoTags || areNoAuthors)
            {
                throw new InvalidLessonException(isTitleEmpty, isDescriptionEmpty, areNoTags, areNoAuthors);
            }

        }

        public Lesson() { }
    }

}
