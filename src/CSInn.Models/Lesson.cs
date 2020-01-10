/*
 * should we allow users to upload attachments that they may think help users in theirendeavor for learning? 
 * 
 * Example: User uploads a database for another user to use, or uploads code snippets/examples for users
 *          to play with or understand the code?
 *          
 */ 



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSInn.Domain.Models.Content.Exceptions;

namespace CSInn.Domain.Models.Content
{
    public class Lesson
    {
        public string Title { get; }
        public string Description { get; set; }
        public string Video { get; set; }
        public string Slides { get; set; }
        public IList<string> Tags { get; set; }
        public IList<string> Authors { get; }

        public Lesson(string title, string description, IEnumerable<string> tags, IEnumerable<string> authors)
        {
            Title = title;
            Description = description;
            Tags = tags?.ToList();
            Authors = authors?.ToList();

            var isTitleEmpty = string.IsNullOrEmpty(title);
            var isDescriptionEmpty = string.IsNullOrEmpty(description);
            var areNoTags = Tags == null || !Tags.Any();
            var areNoAuthors = Authors == null || Authors.Any();

            if (isTitleEmpty || isDescriptionEmpty || areNoTags || areNoAuthors)
            {
                throw new InvalidLessonException(isTitleEmpty, isDescriptionEmpty, areNoTags, areNoAuthors);
            }

        }
    }

}
