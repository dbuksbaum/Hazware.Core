using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System;
using Autofac.Core;

// ReSharper disable CheckNamespace
namespace Hazware.Logging.NLog
// ReSharper restore CheckNamespace
{
  [Export(MefExportTag, typeof(IModule))]
  public sealed class NLogRegistrationModule : AbstractLoggingRegistrationModule
  {
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the NLogRegistrationModule class.
    /// </summary>
    public NLogRegistrationModule()
      : base(typeof(NLogLogger<>))
    {
    }
    #endregion
  }
}