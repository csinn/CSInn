using System;
using System.Linq;
using System.Linq.Expressions;
using CSInn.Domain.Repositories.Extensions;
using CSInn.Domain.Repositories.Specifications.Base;

// TODO: References should be internals visible to this one.
namespace CSInn.Infrastructure.Repositories.Expressions
{
    internal abstract class ExpressionVisitor<TEntity, TVisitor, TItem>
                            where TVisitor : ISpecificationVisitor<TVisitor, TItem>
    {
        public Expression<Func<TEntity, bool>> Expression { get; protected set; }

        public abstract Expression<Func<TEntity, bool>> ConvertSpecToExpression(ISpecification<TItem, TVisitor> spec);

        public void Visit(AndSpecification<TItem, TVisitor> spec)
        {
            var left = ConvertSpecToExpression(spec.Left);
            var right = ConvertSpecToExpression(spec.Right);

            Expression = left.AndAlso(right);
        }

        public void Visit(OrSpecification<TItem, TVisitor> spec)
        {
            var left = ConvertSpecToExpression(spec.Left);
            var right = ConvertSpecToExpression(spec.Right);

            Expression = left.OrElse(right);
        }

        public void Visit(NotSpecification<TItem, TVisitor> spec)
        {
            var specExpr = ConvertSpecToExpression(spec.Specification);
            Expression = specExpr.Not();
        }
    }
}

