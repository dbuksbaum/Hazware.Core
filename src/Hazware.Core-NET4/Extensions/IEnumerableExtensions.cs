using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System;

namespace Hazware.Extensions
{
  /// <summary>
  /// Extension methods for IEnumerable.
  /// </summary>
// ReSharper disable InconsistentNaming
  public static class IEnumerableExtensions
// ReSharper restore InconsistentNaming
  {
    #region ForEach
    /// <summary>
    /// Invokes the specified action for each element in the source. 
    /// ForEach for enumerations. Derived from the following StackOverflow post
    /// http://stackoverflow.com/questions/200574/linq-equivalent-of-foreach-for-ienumerablet
    /// </summary>
    /// <typeparam name="TSource">The type of the elements in source.</typeparam>
    /// <param name="source">The enumerations whose elements will be processed by action.</param>
    /// <param name="action">The action to invoke on each element.</param>
    public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
    {
      Contract.Requires<ArgumentNullException>(source != null);
      Contract.Requires<ArgumentNullException>(action != null);

      foreach (var item in source)
      {
        action(item);
      }
    }
    /// <summary>
    /// Invokes the specified action for each element in the source that passes the specified conditional filter.
    /// ForEach for enumerations. Derived from the following StackOverflow post
    /// http://stackoverflow.com/questions/200574/linq-equivalent-of-foreach-for-ienumerablet
    /// </summary>
    /// <typeparam name="TSource">The type of the elements in source.</typeparam>
    /// <param name="source">The enumerations whose elements will be processed by action.</param>
    /// <param name="action">The action to invoke on each element.</param>
    /// <param name="conditionalFilter">The condition function used to test each element.</param>
    public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action, Func<TSource, bool> conditionalFilter)
    {
      Contract.Requires<ArgumentNullException>(source != null);
      Contract.Requires<ArgumentNullException>(action != null);

      if (conditionalFilter == null)
      { //  if no filter, just execute on all
        source.ForEach(action);
        return;
      }

      foreach(var item in source.Where(conditionalFilter))
      {
        action(item);
      }
    }
    #endregion

    #region ForEachStep
    public static IEnumerable<TSource> ForEachStep<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
    {
      if (source == null) throw new ArgumentNullException("source");
      if (action == null) throw new ArgumentNullException("action");
      //Contract.Requires<ArgumentNullException>(source != null);
      //Contract.Requires<ArgumentNullException>(action != null);

      foreach(var item in source)
      {
        action(item);
        yield return item;
      }
    }
    public static IEnumerable<TSource> ForEachStep<TSource>(this IEnumerable<TSource> source, Action<TSource> action,
                                                            Func<TSource, bool> conditionalFilter)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (action == null)
        throw new ArgumentNullException("action");
      //Contract.Requires<ArgumentNullException>(source != null);
      //Contract.Requires<ArgumentNullException>(action != null);

      if(conditionalFilter == null)
        conditionalFilter = item => true;

      foreach(var item in source.Where(conditionalFilter))
      {
        action(item);
        yield return item;
      }
    }
    #endregion

    #region Paging
    /// <summary>
    /// Paging extensions for enumerations. Derived from the following StackOverflow post:
    /// http://stackoverflow.com/questions/6185159/linq-and-pagination
    /// </summary>
    /// <typeparam name="TSource">The type of the elements in source.</typeparam>
    /// <param name="source">The enumerations whose elements will be paged.</param>
    /// <param name="page">The page number to return.</param>
    /// <param name="pageSize">The size of each page.</param>
    /// <returns></returns>
    public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int page, int pageSize)
    {
      Contract.Requires<ArgumentNullException>(source != null);
      Contract.Requires<ArgumentOutOfRangeException>(page >= 0);
      Contract.Requires<ArgumentOutOfRangeException>(pageSize >= 1);
      return source.Skip((page - 1) * pageSize).Take(pageSize);
    }
    #endregion

    #region Append / Prepend
    /// <summary>
    /// http://lostechies.com/jimmybogard/2009/10/16/more-missing-linq-operators/
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> source, TSource element)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (element == null)
        throw new ArgumentNullException("element");
      //Contract.Requires<ArgumentNullException>(source != null);
      //Contract.Requires<ArgumentNullException>(element != null);
      //Contract.Ensures((bool)(Contract.Result<IEnumerable<TSource>>() != null), "Contract.Result<IEnumerable<TSource>>() != null");

      using (IEnumerator<TSource> e1 = source.GetEnumerator())
        while(e1.MoveNext())
          yield return e1.Current;

      yield return element;
    }

    /// <summary>
    /// http://lostechies.com/jimmybogard/2009/10/16/more-missing-linq-operators/
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Prepend<TSource>(this IEnumerable<TSource> source, TSource element)
    {
      if (source == null)
        throw new ArgumentNullException("source");
      if (element == null)
        throw new ArgumentNullException("element");
      //Contract.Requires<ArgumentNullException>(source != null);
      //Contract.Requires<ArgumentNullException>(element != null);
      //Contract.Ensures((bool)(Contract.Result<IEnumerable<TSource>>() != null), "Contract.Result<IEnumerable<TSource>>() != null");
      yield return element;

      using(IEnumerator<TSource> e1 = source.GetEnumerator())
        while(e1.MoveNext())
          yield return e1.Current;
    }
    #endregion
  }
}