using React.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace React.DAL.Implementation.Common
{
    public static class QueryBuilder
    {
        public static IQueryable<T> ApplyFilters<T>(IQueryable<T> query, FilterDto? filterDto)
        {
            if (filterDto == null)
                return query;

            var parameter = Expression.Parameter(typeof(T), "x");

            foreach (var kv in filterDto.Predicates)
            {
                var propertyName = kv.Key;
                var propertyValue = kv.Value;

                var property = Expression.PropertyOrField(parameter, propertyName);
                var constant = Expression.Constant(Convert.ChangeType(propertyValue, property.Type));
                var condition = Expression.Equal(property, constant);
                var lambda = Expression.Lambda<Func<T, bool>>(condition, parameter);

                query = query.Where(lambda);
            }
            #region Check only in string
            //if (!string.IsNullOrWhiteSpace(filterDto.SearchText))
            //{
            //    var lowerKeyword = filterDto.SearchText.ToLower();

            //    var stringProps = typeof(T)
            //        .GetProperties()
            //        .Where(p => p.PropertyType == typeof(string))
            //        .ToList();

            //    if (stringProps.Any())
            //    {
            //        Expression? combined = null;

            //        foreach (var prop in stringProps)
            //        {
            //            var propAccess = Expression.Property(parameter, prop);
            //            var nullCheck = Expression.NotEqual(propAccess, Expression.Constant(null, typeof(string)));

            //            var toLowerCall = Expression.Call(propAccess, typeof(string).GetMethod("ToLower", Type.EmptyTypes)!);
            //            var containsCall = Expression.Call(
            //                toLowerCall,
            //                typeof(string).GetMethod("Contains", new[] { typeof(string) })!,
            //                Expression.Constant(lowerKeyword)
            //            );

            //            var notNullAndContains = Expression.AndAlso(nullCheck, containsCall);
            //            combined = combined == null ? notNullAndContains : Expression.OrElse(combined, notNullAndContains);
            //        }

            //        if (combined != null)
            //        {
            //            var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);
            //            query = query.Where(lambda);
            //        }
            //    }
            //} 
            #endregion

            if (!string.IsNullOrWhiteSpace(filterDto.SearchText))
            {
                var keyword = filterDto.SearchText.ToLower();
                Expression? combined = null;

                foreach (var prop in typeof(T).GetProperties())
                {
                    var propertyAccess = Expression.Property(parameter, prop);

                    // property != null
                    var notNullCheck = prop.PropertyType.IsValueType && Nullable.GetUnderlyingType(prop.PropertyType) == null
                        ? (Expression?)null // non-nullable value types don't need null check
                        : Expression.NotEqual(propertyAccess, Expression.Constant(null));

                    // Convert property to string using .ToString()
                    var toStringCall = Expression.Call(propertyAccess, "ToString", Type.EmptyTypes);
                    var toLowerCall = Expression.Call(toStringCall, typeof(string).GetMethod("ToLower", Type.EmptyTypes)!);

                    // Contains(keyword)
                    var containsCall = Expression.Call(
                        toLowerCall,
                        typeof(string).GetMethod("Contains", new[] { typeof(string) })!,
                        Expression.Constant(keyword)
                    );

                    // Full condition: (prop != null) && prop.ToString().ToLower().Contains(keyword)
                    Expression condition = notNullCheck != null
                        ? Expression.AndAlso(notNullCheck, containsCall)
                        : containsCall;

                    combined = combined == null ? condition : Expression.OrElse(combined, condition);
                }

                if (combined != null)
                {
                    var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);
                    query = query.Where(lambda);
                }
            }

            if (filterDto.SortModels != null && filterDto.SortModels.Any())
            {
                bool firstSort = true;

                foreach (var sort in filterDto.SortModels)
                {
                    var property = typeof(T).GetProperty(sort.Field);
                    if (property == null) continue;

                    var sortParam = Expression.Parameter(typeof(T), "x");
                    var propertyAccess = Expression.MakeMemberAccess(sortParam, property);
                    var orderByExp = Expression.Lambda(propertyAccess, sortParam);

                    string methodName = sort.Sort.ToLower() == "desc"
                        ? (firstSort ? "OrderByDescending" : "ThenByDescending")
                        : (firstSort ? "OrderBy" : "ThenBy");

                    var resultExp = Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new Type[] { typeof(T), property.PropertyType },
                        query.Expression,
                        Expression.Quote(orderByExp)
                    );

                    query = query.Provider.CreateQuery<T>(resultExp);
                    firstSort = false;
                }
            }

            if (filterDto.PageNo > 0 && filterDto.PageSize > 0)
            {
                int skip = (filterDto.PageNo - 1) * filterDto.PageSize;
                query = query.Skip(skip).Take(filterDto.PageSize);
            }
            return query;
        }

        public static void AddPredicateIfNotNull<T>(
           this FilterDto filter,
           string propertyName,
           T? value)
        {
            if (value == null)
                return;

            if (value is string str && string.IsNullOrWhiteSpace(str))
                return;

            filter.Predicates.Add(propertyName, value);
        }
    }
}
