﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SmartHomeAutomation.Services
{
    public static class Utilities
    {
        public static Expression<Func<TEntity, bool>> BuildLambdaForFindByKey<TEntity>(Guid guid)
            => BuildEqualsExpression<TEntity>(typeof(TEntity).Name + "Id", guid);

        public static Expression<Func<TEntity, bool>> BuildEqualsExpression<TEntity>(string propertyName, object inputValue)
        {
            var item = Expression.Parameter(typeof(TEntity), "entity");
            var lambda = BuildEqualsExpression<TEntity>(item, new Helper { PropertyName = propertyName, InputValue = inputValue });

            return lambda;
        }

        public static IEnumerable<Expression<Func<TEntity, bool>>> BuildEqualsExpression<TEntity>(params Helper[] inputs)
        {
            var paramExpression = Expression.Parameter(typeof(TEntity), "entity");
            return inputs.Select(input => BuildEqualsExpression<TEntity>(paramExpression, input));
        }

        public static IEnumerable<Expression<Func<TEntity, bool>>> BuildContainsExpression<TEntity>(params Helper[] inputs)
        {
            var paramExpression = Expression.Parameter(typeof(TEntity), "entity");
            return inputs.Select(input => BuildContainsExpression<TEntity>(paramExpression, input));
        }

        public static Expression<Func<TEntity, bool>> CombineExpressions<TEntity>(IEnumerable<Expression<Func<TEntity, bool>>> expressions)
        {
            var enumerable = expressions.ToList();
            return enumerable.Count == 1
                ? enumerable.Single()
                : Expression.Lambda<Func<TEntity, bool>>(enumerable.Select(i => i.Body).Aggregate(Expression.Or),
                    enumerable.ElementAt(0).Parameters);
        }

        private static Expression<Func<TEntity, bool>> BuildContainsExpression<TEntity>(ParameterExpression parameterExpression, Helper input)
        {
            var propertyExp = Expression.Property(parameterExpression, input.PropertyName);
            var someValue = Expression.Constant(input.InputValue, typeof(string));
            var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var containsMethodExp = Expression.Call(propertyExp, method, someValue);

            return Expression.Lambda<Func<TEntity, bool>>(containsMethodExp, parameterExpression);
        }

        private static Expression<Func<TEntity, bool>> BuildEqualsExpression<TEntity>(ParameterExpression parameterExpression, Helper input)
        {
            var prop = Expression.Property(parameterExpression, input.PropertyName);
            var value = Expression.Constant(input.InputValue);
            var equal = Expression.Equal(prop, value);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, parameterExpression);

            return lambda;
        }

        public class Helper
        {
            public string PropertyName { get; set; }
            public object InputValue { get; set; }
        }
    }
}