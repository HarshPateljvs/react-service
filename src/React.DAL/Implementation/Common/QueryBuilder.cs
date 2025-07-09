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

            filter.Predicates.Add(propertyName,value);
        }
    }
}
