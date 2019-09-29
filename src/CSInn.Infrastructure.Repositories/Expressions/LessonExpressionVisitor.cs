using System;
using System.Linq;
using System.Linq.Expressions;
using CSInn.Domain.Repositories.Specifications.Base;
using CSInn.Domain.Repositories.Specifications.Lesson;
using CSInn.Infrastructure.Repositories.Entities;
using CSInn.Models;

namespace CSInn.Infrastructure.Repositories.Expressions
{
    internal class LessonEntityExpressionVisitor : ExpressionVisitor<LessonEntity, ILessonSpecificationVisitor, Lesson>, ILessonSpecificationVisitor
    {
        public override Expression<Func<LessonEntity, bool>> ConvertSpecToExpression(ISpecification<Lesson, ILessonSpecificationVisitor> spec)
        {
            var visitor = new LessonEntityExpressionVisitor();
            spec.Accept(visitor);
            return visitor.Expression;
        }

        public void Visit(TitleLike spec) => Expression = e => e.Title.Contains(spec.Title);
        public void Visit(TagMatches spec) => Expression = e => e.Tags.Contains(spec.Tag);
        public void Visit(AuthorLike authorLike) => Expression = e => e.Authors.Contains(authorLike.Author);
    }
}
