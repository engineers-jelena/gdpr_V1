using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GDPRiS.Common.Helpers
{
    public static class SortHelper
    {
        public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string sortField, bool desc)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, sortField);
            var exp = Expression.Lambda(prop, param);
            string method = !desc ? "OrderBy" : "OrderByDescending";
            Type[] types = new Type[] { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(mce);
        }

        //public static Func<IQueryable<T>, IOrderedQueryable<T>> GetOrderBy<T>(string sort, bool desc) where T : class
        //{
        //    if (string.IsNullOrEmpty(sort))
        //        throw new ApplicationException("Sort parameter missing!");

        //    PropertyInfo[] properties = typeof(T).GetProperties();
        //    PropertyInfo propertyToSortBy = null;
        //    foreach (PropertyInfo property in properties)
        //    {
        //        if (property.Name.ToLower() == sort.ToLower())
        //        {
        //            propertyToSortBy = property;
        //        }
        //    }

        //    if (propertyToSortBy == null)
        //        throw new ApplicationException("Class " + typeof (T).FullName + " has no property \"" + sort + "\"!");

        //    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = delegate (IQueryable<T> t)
        //    {
        //        PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(T)).Find(propertyToSortBy.Name, true);
        //        return !desc ? t.OrderBy(x => prop.GetValue(x)) : t.OrderByDescending(x => prop.GetValue(x));
        //    };

        //    return orderBy;
        //}
    }
}
