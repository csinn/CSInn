using System;
using System.Linq.Expressions;
using CSInn.Domain.Repositories.Specifications.Base;

namespace CSInn.Domain.Repositories.Specifications.Lesson
{
    public class TitleLike : ISpecification<CSInn.Models.Lesson, ILessonSpecificationVisitor>
    {
        public string Title { get; }

        public TitleLike(string title)
        {
            Title = title;
        }

        public bool IsSatisfiedBy(CSInn.Models.Lesson lesson) => lesson.Title.Contains(Title);

        public void Accept(ILessonSpecificationVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
