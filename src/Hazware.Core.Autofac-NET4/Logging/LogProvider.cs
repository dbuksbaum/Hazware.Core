using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System;

namespace Hazware.Logging
{
  internal class LogProvider : ILogProvider
  {
    #region Fields
    private readonly Func<Type, ILog> _logCreator;
    #endregion

    #region Constructors
    public LogProvider(Func<Type, ILog> creator)
    {
      Contract.Requires<ArgumentNullException>(creator != null);
      _logCreator = creator;
    }

    #endregion

    #region IProvider<string,ILog> Members
    public ILog Get(Type key)
    {
      return _logCreator(key);
    }
    #endregion
  }
}