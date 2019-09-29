using System;
using System.Collections.Generic;
using System.Text;

namespace CSInn.Domain.Repositories.Specifications.Base
{
    public interface ISpecification<in T, in TVisitor> where TVisitor : ISpecificationVisitor<TVisitor, T>
    {
        bool IsSatisfiedBy(T item);
        void Accept(TVisitor visitor);
    }
}
