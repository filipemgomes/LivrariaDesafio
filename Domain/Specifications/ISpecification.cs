using System;
using System.Linq.Expressions;

namespace Domain.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> ToExpression();
    }
}
