using System.Collections.Generic;
using System.Linq;
using System;

namespace Hazware
{
  /// <summary>
  /// A modified form of <seealso cref="DisposableAction"/> that changes
  /// the default IDisposable pattern to make it repeatably callable. Disposal
  /// is not flagged as completed, so IsDisposed will allways be false, allowing
  /// this instance to be called many times. Useful for counted disposal patterns 
  /// in which the actual disposal does not take place in this action, but a 
  /// counter is decremented.
  /// </summary>
  public class RepeatableDisposableAction : DisposableAction
  {
    #region Constructor
    /// <summary>
    /// Initializes a new instance of the <see cref="RepeatableDisposableAction"/> class.
    /// </summary>
    /// <param name="action">The action.</param>
    public RepeatableDisposableAction(Action action)
      : base(action)
    {
    }
    #endregion

    #region DisposableAction Overrides
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
    public new void Dispose()
    {
      Dispose(true);
    }
    protected override void Dispose(bool disposing)
    { //  we dont call the base class, so the class is never marked as disposed
      //  and can be called again
      if (disposing)
      { // Dispose managed resources.
        DisposeManagedResources();
      }

      // There are no unmanaged resources to release, but
      // if we add them, they need to be released here.
      DisposeUnmanagedResources();
    }
    #endregion
  }
}