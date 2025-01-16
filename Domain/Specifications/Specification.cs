using System;
using System.Linq.Expressions;

namespace Domain.Specifications
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();

        public Func<T, bool> IsSatisfiedBy => ToExpression().Compile();
    }
}
