using System.Collections.Generic;
using System.Linq;
using System;

namespace Hazware.Extensions
{
  /// <summary>
  /// Extension methods for IQueryable
  /// </summary>
// ReSharper disable InconsistentNaming
  public static class IQueryableExtensions
// ReSharper restore InconsistentNaming
  {
    /// <summary>
    /// Paging extensions for queries. Derived from the following StackOverflow post:
    /// http://stackoverflow.com/questions/6185159/linq-and-pagination
    /// </summary>
    /// <typeparam name="TSource">The type of the elements in source.</typeparam>
    /// <param name="source">The queryable whose elements will be paged.</param>
    /// <param name="page">The page number to return.</param>
    /// <param name="pageSize">The size of each page.</param>
    /// <returns></returns>
    public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int page, int pageSize)
    {
      return source.Skip((page - 1) * pageSize).Take(pageSize);
    }
  }
}