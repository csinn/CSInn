using System;
using System.Linq;
using System.Linq.Expressions;
using CSInn.Domain.Repositories.Specifications.Base;
using CSInn.Models;

namespace CSInn.Domain.Repositories.Specifications.Lesson
{
    public class AuthorLike : ISpecification<CSInn.Models.Lesson, ILessonSpecificationVisitor>
    {
        public string Author { get; }

        public AuthorLike(string author)
        {
            Author = author;
        }

        public void Accept(ILessonSpecificationVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsSatisfiedBy(CSInn.Models.Lesson lesson) => lesson.Authors.Any(a => a.Contains(Author));
    }
}
