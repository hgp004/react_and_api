using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace dotnet_core_api_react.Extensions
{
    public static class QueryableExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> queryable, string propertyName, Nullable<bool> descending = null)
        {
            if (queryable == null)
                throw new ArgumentNullException(nameof(queryable));

            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));

            Type elementType = typeof(T);
            PropertyInfo property = elementType.GetProperty(propertyName);
            if (property == null)
                throw new ArgumentException($"属性[{propertyName}]无效!", nameof(propertyName));

            ParameterExpression parameter = Expression.Parameter(elementType);
            Expression body = Expression.MakeMemberAccess(parameter, property);
            Type @delegate = typeof(Func<,>).MakeGenericType(elementType, property.PropertyType);
            Expression lambda = Expression.Lambda(@delegate, body, parameter);

            Func<IQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>> orderby = default;
            if (descending.HasValue && descending.Value)
                orderby = new Func<IQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>>(Queryable.OrderByDescending);
            else orderby = new Func<IQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>>(Queryable.OrderBy);

            MethodInfo method = orderby.GetMethodInfo().GetGenericMethodDefinition();
            method = method.MakeGenericMethod(elementType, property.PropertyType);
            Expression result = Expression.Call(method, queryable.Expression, Expression.Quote(lambda));
            return queryable.Provider.CreateQuery<T>(result) as IOrderedQueryable<T>;
        }
    }
}
