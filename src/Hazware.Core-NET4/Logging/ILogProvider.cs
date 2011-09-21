using System.Collections.Generic;
using System.Linq;
using System;
using Hazware.Collections.Generic;

namespace Hazware.Logging
{
  ///<summary>
  /// Interface used for providing <see cref="ILog"/> instances.
  ///</summary>
  public interface ILogProvider : IProvider<Type, ILog>
  {
  }
}