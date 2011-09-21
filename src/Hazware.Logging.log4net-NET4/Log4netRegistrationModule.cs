using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System;
using Autofac;
using Autofac.Core;
using Config = log4net.Config;

// ReSharper disable CheckNamespace
namespace Hazware.Logging.log4net
// ReSharper restore CheckNamespace
{
  [Export(MefExportTag, typeof(IModule))]
  public class Log4NetRegistrationModule : AbstractLoggingRegistrationModule
  {
    #region Constants
    private const string Log4Net_Config_File = "log4net.config";
    private const string Log4Net_Dll_Config_File = "log4net.dll.config";
    private const string Log4Net_Global_Environment_Var = "LOG4NET_GLOBAL_CONFIG_FILE";
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the Log4netRegistrationModule class.
    /// </summary>
    public Log4NetRegistrationModule()
      : base(typeof(Log4NetLogger<>))
    {
    }
    #endregion

    #region Overrides
    protected override void OnAfterRegistration(ContainerBuilder builder)
    { //  configure logging 
      //  find the log4net configuration
      string fileToWatch = FindConfigFile();

      //  if we have a config file, configure with it
      if (!string.IsNullOrEmpty(fileToWatch))
        Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(fileToWatch));
    }
    #endregion

    #region Private Methods
    private string FindConfigFile()
    {
      try
      {
        //  TODO: Fix This
        var appPath = string.Empty; //ApplicationEnvironment.ApplicationPath;
        var appExe = string.Empty; //ApplicationEnvironment.ApplicationFile;

        //  try log4net default first
        var defaultConfig = Path.Combine(appPath, Log4Net_Config_File);
        if (File.Exists(defaultConfig))
          return defaultConfig;

        //  see if there is a log4net.dll config
        defaultConfig = Path.Combine(appPath, Log4Net_Dll_Config_File);
        if (File.Exists(defaultConfig))
          return defaultConfig;

        //  check for 'application.exe.log4net'
        defaultConfig = string.Format("{0}.log4net", appExe);
        if (File.Exists(defaultConfig))
          return defaultConfig;

        //  check if the global environment variable is set
        defaultConfig = Environment.ExpandEnvironmentVariables(Log4Net_Global_Environment_Var);
        if (!string.IsNullOrEmpty(defaultConfig) && File.Exists(defaultConfig))
          return defaultConfig;

        //  checked everywhere else, so last chance is that it is in the app.config
        //  check for 'application.exe.config'
        defaultConfig = string.Format("{0}.config", appExe);
        if (File.Exists(defaultConfig))
          return defaultConfig;
      }
      catch (Exception)
      {
        //  do nothing, we will return null
      }

      //  not found, so do not configure
      return null;
    }
    #endregion
  }
}