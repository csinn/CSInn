using CSInn.Domain.Repositories.Specifications.Base;

namespace CSInn.Domain.Repositories.Specifications.Lesson
{
    public class TagMatches : ISpecification<CSInn.Models.Lesson, ILessonSpecificationVisitor>
    {
        public string Tag { get; }

        public TagMatches(string tag)
        {
            Tag = tag;
        }

        public void Accept(ILessonSpecificationVisitor visitor)
        {
            visitor.Visit(this);
        }

        public bool IsSatisfiedBy(CSInn.Models.Lesson lesson) => lesson.Tags.Contains(Tag);
    }
}