﻿using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SmartHomeAutomation.Services.Helpers
{
    public static class QueryHelper
    {
        public static bool PropertyExists<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                                                        BindingFlags.Public | BindingFlags.Instance) != null;
        }

        public static dynamic TypeOfProperty<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase |
                                                        BindingFlags.Public | BindingFlags.Instance).PropertyType;
        }

        public static Expression<Func<T, string>> GetPropertyExpression<T>(string propertyName)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return null;
            }

            var paramterExpression = Expression.Parameter(typeof(T));
            return (Expression<Func<T, string>>)Expression.Lambda(Expression.PropertyOrField(paramterExpression, propertyName), paramterExpression);

        }

        public static Expression<Func<T, int>> GetPropertyExpressionInt<T>(string propertyName)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return null;
            }
            var paramterExpression = Expression.Parameter(typeof(T));
            return (Expression<Func<T, int>>)Expression.Lambda(Expression.PropertyOrField(paramterExpression, propertyName), paramterExpression);
        }
    }
}
