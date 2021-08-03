using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BLL.Extentions
{
    public static class ExpressionExtentions
    {
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
         where T : class
        {
            var leftParam = left.Parameters[0];
            var rightParam = right.Parameters[0];

            return  Expression.Lambda<Func<T, bool>>(
                    Expression.OrElse(
                        left.Body,
                        new ParameterReplacer(rightParam, leftParam).Visit(right.Body)),
                    leftParam);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
            where T : class
        {
            var leftParam = left.Parameters[0];
            var rightParam = right.Parameters[0];

            return 
                Expression.Lambda<Func<T, bool>>(
                    Expression.AndAlso(
                        left.Body,
                        new ParameterReplacer(rightParam, leftParam).Visit(right.Body)),
                    leftParam);
        }

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
            where T : class
        {
            return Expression.Lambda<Func<T, bool>>(
                    Expression.Not(expression.Body),
                    expression.Parameters);
        }
    }
    internal class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression parameter;
        private readonly ParameterExpression replacement;

        public ParameterReplacer(ParameterExpression parameter, ParameterExpression replacement)
        {
            this.parameter = parameter;
            this.replacement = replacement;
        }

        protected override Expression VisitParameter(ParameterExpression node) => base.VisitParameter(this.parameter == node ? this.replacement : node);
    }
}
