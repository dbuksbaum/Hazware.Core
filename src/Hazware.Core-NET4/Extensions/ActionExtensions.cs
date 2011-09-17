using System.Collections.Generic;
using System.Linq;
using System;
#if !SILVERLIGHT
using System.Threading;
using System.Threading.Tasks;
#endif

namespace Hazware.Extensions
{
  public static class ActionExtensions
  {
    /// <summary>
    /// Converts the action into <see cref="DisposableAction"/>
    /// </summary>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    public static IDisposable AsDisposable(this Action action)
    {
      return new DisposableAction(action);
    }

#if !SILVERLIGHT
    public static Task AsTask(this Action action)
    {
      return new Task(action);
    }
    public static Task AsTask(this Action action, CancellationToken cancellationToken)
    {
      return new Task(action, cancellationToken);
    }
    public static Task AsTask(this Action action, TaskCreationOptions creationOptions)
    {
      return new Task(action, creationOptions);
    }
    public static Task AsTask(this Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
    {
      return new Task(action, cancellationToken, creationOptions);
    }
    public static Task Start(this Action action)
    {
      var task = new Task(action);
      task.Start();
      return task;
    }
    public static Task Start(this Action action, CancellationToken cancellationToken)
    {
      var task = new Task(action, cancellationToken);
      task.Start();
      return task;
    }
    public static Task Start(this Action action, TaskCreationOptions creationOptions)
    {
      var task = new Task(action, creationOptions);
      task.Start();
      return task;
    }
    public static Task Start(this Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions)
    {
      var task = new Task(action, cancellationToken, creationOptions);
      task.Start();
      return task;
    }
#endif
  }
}