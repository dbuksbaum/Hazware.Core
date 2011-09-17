using System.Collections.Generic;
using System.Linq;
using System;

namespace Hazware.Container
{
  public sealed class ContainerConfiguration : IContainerConfiguration
  {
    #region Implementation of IContainerConfiguration
    public bool ResolveAnything { get; set; }
    #endregion
  }
}