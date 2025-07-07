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
            if (filterDto == null || filterDto.Predicates == null || !filterDto.Predicates.Any())
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
