using System.Collections.Generic;
using System.Linq;
using System;
#if !SILVERLIGHT
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Hazware.Extensions
{
  public static class FuncExtensions
  {
#if !SILVERLIGHT
    public static Task<TResult> AsTask<TResult>(this Func<TResult> func)
    {
      return new Task<TResult>(func);
    }
    public static Task<TResult> AsTask<TResult>(this Func<TResult> func, CancellationToken cancellationToken)
    {
      return new Task<TResult>(func, cancellationToken);
    }
    public static Task<TResult> AsTask<TResult>(this Func<TResult> func, TaskCreationOptions creationOptions)
    {
      return new Task<TResult>(func, creationOptions);
    }
    public static Task<TResult> AsTask<TResult>(this Func<TResult> func, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
    {
      return new Task<TResult>(func, cancellationToken, creationOptions);
    }
    public static Task Start<TResult>(this Func<TResult> func)
    {
      var task = new Task<TResult>(func);
      task.Start();
      return task;
    }
    public static Task Start<TResult>(this Func<TResult> func, CancellationToken cancellationToken)
    {
      var task = new Task<TResult>(func, cancellationToken);
      task.Start();
      return task;
    }
    public static Task Start<TResult>(this Func<TResult> func, TaskCreationOptions creationOptions)
    {
      var task = new Task<TResult>(func, creationOptions);
      task.Start();
      return task;
    }
    public static Task Start<TResult>(this Func<TResult> func, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
    {
      var task = new Task<TResult>(func, cancellationToken, creationOptions);
      task.Start();
      return task;
    }
#endif
  }
}