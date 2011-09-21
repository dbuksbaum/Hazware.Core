using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System;
using System.Text;
using Hazware.Interop.Kernel32;

namespace Hazware
{
  /// <summary>
  /// The ApplicationEnvironment class contains information about the currently running application.
  /// </summary>
  public sealed class ApplicationEnvironment
  {
    #region Static Fields
    private readonly static AssemblyVersionInfo ApplicationVersionInfo;
    #endregion

    #region Static Properties
    /// <summary>
    /// Path of currently executing executable, excluding filename.
    /// </summary>
    public static string ApplicationPath { get; private set; }
    /// <summary>
    /// Path of currently executing executable, including filename.
    /// </summary>
    public static string ApplicationFile { get; private set; }
    /// <summary>
    /// Gets the name of the assembly that is the currently running application.
    /// </summary>
    public static string ApplicationName { get; private set; }
    /// <summary>
    /// Gets the version of the assembly that is the currently running application.
    /// </summary>
    public static Version ApplicationVersion { get; private set; }
    /// <summary>
    /// Company name.
    /// </summary>
    public static string CompanyName
    {
      [DebuggerNonUserCode]
      get { return ApplicationVersionInfo.CompanyName; }
    }
    /// <summary>
    /// Product name.
    /// </summary>
    public static string ProductName
    {
      [DebuggerNonUserCode]
      get { return ApplicationVersionInfo.ProductName; }
    }
    /// <summary>
    /// Product description.
    /// </summary>
    public static string ProductDescription
    {
      [DebuggerNonUserCode]
      get { return ApplicationVersionInfo.ProductDescription; }
    }
    /// <summary>
    /// Product version string.
    /// </summary>
    public static string VersionString
    {
      [DebuggerNonUserCode]
      get { return ApplicationVersionInfo.ProductVersionString; }
    }
    /// <summary>
    /// Product copyright.
    /// </summary>
    public static string ProductCopyright
    {
      [DebuggerNonUserCode]
      get { return ApplicationVersionInfo.ProductCopyright; }
    }
    /// <summary>
    /// Path folder where roaming settings for current user must be placed.
    /// </summary>
    public static string CurrentUserAllMachinesSettingsPath { get; private set; }
    /// <summary>
    /// Path folder where non-roaming settings for all users must be placed.
    /// </summary>
    public static string AllUsersCurrentMachineSettingsPath { get; private set; }
    /// <summary>
    /// Path folder where non-roaming settings for current user must be placed.
    /// </summary>
    public static string CurrentUserCurrentMachineSettingsPath { get; private set; }
    #endregion

    #region Static Constructor
    static ApplicationEnvironment()
    {
      var pathApplicationExecutable = new StringBuilder(260);

      SafeNativeMethods.GetModuleFileName(IntPtr.Zero, pathApplicationExecutable, pathApplicationExecutable.Capacity);

      ApplicationFile = pathApplicationExecutable.ToString();
      ApplicationPath = Path.GetDirectoryName(ApplicationFile);
      ApplicationVersionInfo = AssemblyVersionInfo.GetVersionInfo(ApplicationFile, 4);
      ApplicationVersion = ApplicationVersionInfo.ProductVersion;
      ApplicationName = Path.GetFileName(ApplicationFile);

      string format;

      if (ApplicationVersionInfo.CompanyName == null)
      {
        format = "{1}";
      }
      else
      {
        if (ApplicationVersionInfo.ProductName == null)
        {
          format = "{1}{0}{2}";
        }
        else
        {
          if (ApplicationVersionInfo.ProductVersion == null)
          {
            format = "{1}{0}{2}{0}{3}";
          }
          else
          {
            format = "{1}{0}{2}{0}{3}{0}{4}";
          }
        }
      }

      CurrentUserAllMachinesSettingsPath = string.Format(format,
                                                          Path.DirectorySeparatorChar,
                                                          Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                                          ApplicationVersionInfo.CompanyName,
                                                          ApplicationVersionInfo.ProductName,
                                                          ApplicationVersionInfo.ProductVersion);

      AllUsersCurrentMachineSettingsPath = string.Format(format,
                                                          Path.DirectorySeparatorChar,
                                                          Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                                                          ApplicationVersionInfo.CompanyName,
                                                          ApplicationVersionInfo.ProductName,
                                                          ApplicationVersionInfo.ProductVersion);

      CurrentUserCurrentMachineSettingsPath = string.Format(format,
                                                             Path.DirectorySeparatorChar,
                                                             Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                                             ApplicationVersionInfo.CompanyName,
                                                             ApplicationVersionInfo.ProductName,
                                                             ApplicationVersionInfo.ProductVersion);
    }
    #endregion
  }
}