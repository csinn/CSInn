using System;
using System.Linq;
using System.Linq.Expressions;
using CSInn.Domain.Repositories.Specifications.Base;

// TODO: References should be internals visible to this one.
namespace CSInn.Infrastructure.Repositories.Expressions
{
    public abstract class ExpressionVisitor<TEntity, TVisitor, TItem>
                            where TVisitor : ISpecificationVisitor<TVisitor, TItem>
    {
        public Expression<Func<TEntity, bool>> Expression { get; protected set; }

        public abstract Expression<Func<TEntity, bool>> ConvertSpecToExpression(ISpecification<TItem, TVisitor> spec);

        public void Visit(AndSpecification<TItem, TVisitor> spec)
        {
            var leftExpr = ConvertSpecToExpression(spec.Left);
            var rightExpr = ConvertSpecToExpression(spec.Right);

            var exprBody = System.Linq.Expressions.Expression.AndAlso(leftExpr.Body, rightExpr.Body);
            Expression = System.Linq.Expressions.Expression.Lambda<Func<TEntity, bool>>(exprBody, leftExpr.Parameters.Single());
        }

        public void Visit(OrSpecification<TItem, TVisitor> spec)
        {
            var leftExpr = ConvertSpecToExpression(spec.Left);
            var rightExpr = ConvertSpecToExpression(spec.Right);

            var exprBody = System.Linq.Expressions.Expression.Or(leftExpr.Body, rightExpr.Body);
            Expression = System.Linq.Expressions.Expression.Lambda<Func<TEntity, bool>>(exprBody, leftExpr.Parameters.Single());
        }

        public void Visit(NotSpecification<TItem, TVisitor> spec)
        {
            var specExpr = ConvertSpecToExpression(spec.Specification);

            var exprBody = System.Linq.Expressions.Expression.Not(specExpr.Body);
            Expression = System.Linq.Expressions.Expression.Lambda<Func<TEntity, bool>>(exprBody, specExpr.Parameters.Single());
        }
    }
}

