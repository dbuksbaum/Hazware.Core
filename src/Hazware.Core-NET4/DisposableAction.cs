using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System;
using System.Runtime.InteropServices;

namespace Hazware
{
  /// <summary>
  /// Calls a delegate during Disposal
  /// </summary>
  public class DisposableAction : AbstractDisposable
  {
    #region Fields
    private readonly Action _disposalAction;
    private readonly DisposerExecutionCondition _executionCondition;
    #endregion

    #region Properties
    /// <summary>
    /// Gets the execution condition.
    /// </summary>
    /// <value>The execution condition.</value>
    public DisposerExecutionCondition ExecutionCondition { get { return (_executionCondition); } }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="DisposableAction"/> class.
    /// </summary>
    /// <param name="action">The action.</param>
    public DisposableAction(Action action)
      : this(action, DisposerExecutionCondition.Unconditional)
    {
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="DisposableAction"/> class.
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="executionCondition">The execution condition.</param>
    public DisposableAction(Action action, DisposerExecutionCondition executionCondition)
    {
      Contract.Requires<ArgumentNullException>(action != null);
      _disposalAction = action;
      _executionCondition = executionCondition;
    }
    #endregion

    #region AbstractDisposable
    /// <summary>
    /// Disposes the unmanaged resources. This is called at the right time in the disposal process.
    /// </summary>
    protected override void DisposeUnmanagedResources()
    {
    }
    /// <summary>
    /// Disposes the managed resources. This is called at the right time in the disposal process.
    /// </summary>
    [DebuggerStepThrough]
    protected override void DisposeManagedResources()
    {
      if (ShouldCallCustomAction())
      {
        _disposalAction();
      }
    }
    #endregion

    #region Helper Methods
    private static bool ExceptionHasBeenThrown()
    {
#if !SILVERLIGHT
      return (Marshal.GetExceptionCode() != 0);
#else
      // GetExceptionCode is not available in Silverlight, so we only call the action when 
      //  the DisposerExecutionCondition is Unconditional
      return false;
#endif
    }
    private bool ShouldCallCustomAction()
    {
      return ((_executionCondition == DisposerExecutionCondition.Unconditional) || !ExceptionHasBeenThrown());
    }
    #endregion
  }
}