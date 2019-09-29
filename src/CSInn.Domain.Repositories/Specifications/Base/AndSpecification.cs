using System;
using System.Collections.Generic;
using System.Text;

namespace CSInn.Domain.Repositories.Specifications.Base
{
    public class AndSpecification<T, TVisitor> : ISpecification<T, TVisitor>
        where TVisitor : ISpecificationVisitor<TVisitor, T>
    {
        public ISpecification<T, TVisitor> Left { get; }
        public ISpecification<T, TVisitor> Right { get; }

        public AndSpecification(ISpecification<T, TVisitor> left, ISpecification<T, TVisitor> right)
        {
            this.Left = left;
            this.Right = right;
        }

        public void Accept(TVisitor visitor) => visitor.Visit(this);
        public bool IsSatisfiedBy(T obj) => Left.IsSatisfiedBy(obj) && Right.IsSatisfiedBy(obj);
    }
}
