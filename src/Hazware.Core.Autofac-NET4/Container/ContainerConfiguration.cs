using System.Collections.Generic;
using System.Linq;
using System;
using Hazware.Logging;

namespace Hazware.Container
{
  public sealed class ContainerConfiguration : IContainerConfiguration
  {
    #region Implementation of IContainerConfiguration
    public bool ResolveAnything { get; set; }
    public ILogProvider LogProvider { get; set; }
    #endregion
  }
}