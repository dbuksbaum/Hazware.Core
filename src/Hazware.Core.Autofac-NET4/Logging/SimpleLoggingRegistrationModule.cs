using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System;

namespace Hazware.Logging
{
  public sealed class SimpleLoggingRegistrationModule : AbstractLoggingRegistrationModule
  {
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the SimpleLoggingRegistrationModule class.
    /// </summary>
//    public SimpleLoggingRegistrationModule(Type logWrapper)
//      : base(logWrapper)
//    {
//    }
    public SimpleLoggingRegistrationModule(Func<Type, ILog> creator)
      : base(creator)
    {
      Contract.Requires<ArgumentNullException>(creator != null);
    }
    public SimpleLoggingRegistrationModule(ILogProvider provider)
      : base(provider)
    {
      Contract.Requires<ArgumentNullException>(provider != null);
    }
    #endregion
  }
}