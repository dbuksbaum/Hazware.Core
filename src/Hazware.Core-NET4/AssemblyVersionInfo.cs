using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System;
using System.Reflection;

namespace Hazware
{
  /// <summary>
  /// Provides information about assembly version.
  /// </summary>
  public sealed class AssemblyVersionInfo  
  {
    #region Fields
    private readonly string _companyName;
    private readonly string _productName;
    private readonly string _productDescription;
    private readonly string _productVersionString;
    private readonly string _productCopyright;
    private readonly Version _productVersion;
    #endregion

    #region Properties
    /// <summary>
    /// Company name.
    /// </summary>
    public string CompanyName
    {
      [DebuggerStepThrough]
      get { return _companyName; }
    }
    /// <summary>
    /// Product name.
    /// </summary>
    public string ProductName
    {
      [DebuggerStepThrough]
      get { return _productName; }
    }
    /// <summary>
    /// Product description.
    /// </summary>
    public string ProductDescription
    {
      [DebuggerStepThrough]
      get { return _productDescription; }
    }
    /// <summary>
    /// Product version string.
    /// </summary>
    public string ProductVersionString
    {
      [DebuggerStepThrough]
      get { return _productVersionString; }
    }
    /// <summary>
    /// Product version.
    /// </summary>
    public Version ProductVersion
    {
      [DebuggerStepThrough]
      get { return _productVersion; }
    }
    /// <summary>
    /// Product copyright.
    /// </summary>
    public string ProductCopyright
    {
      [DebuggerStepThrough]
      get { return _productCopyright; }
    }
    #endregion

    #region Methods
    private AssemblyVersionInfo(string path, int fieldCount)
    {
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(path));
      Contract.Requires<IndexOutOfRangeException>((fieldCount >= 1) && (fieldCount <= 4));
      Assembly assembly = Assembly.ReflectionOnlyLoadFrom(path);

      foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(assembly))
      {
        if (customAttributeData.Constructor.DeclaringType == typeof(AssemblyCompanyAttribute))
        {
          _companyName = customAttributeData.ConstructorArguments.First().Value as string;
        }
        else if (customAttributeData.Constructor.DeclaringType == typeof(AssemblyProductAttribute))
        {
          _productName = customAttributeData.ConstructorArguments.First().Value as string;
        }
        else if (customAttributeData.Constructor.DeclaringType == typeof(AssemblyDescriptionAttribute))
        {
          _productDescription = customAttributeData.ConstructorArguments.First().Value as string;
        }
        else if (customAttributeData.Constructor.DeclaringType == typeof(AssemblyCopyrightAttribute))
        {
          _productCopyright = customAttributeData.ConstructorArguments.First().Value as string;
        }
      }

      _productVersion = assembly.GetName().Version;
      _productVersionString = _productVersion.ToString(fieldCount);
    }

    /// <summary>
    /// Returns assembly version information for an assembly with given path.
    /// </summary>
    /// <param name="path">Path to assembly file.</param>
    /// <param name="fieldCount">Number of fields in version string. Must be from 1 to 4.</param>
    /// <returns>AssemblyVersionInfo</returns>
    public static AssemblyVersionInfo GetVersionInfo(string path, int fieldCount)
    {
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(path));
      Contract.Requires<IndexOutOfRangeException>((fieldCount >= 1) && (fieldCount <= 4));
      return new AssemblyVersionInfo(path, fieldCount);
    }

    /// <summary>
    ///  Returns assembly version information for an assembly with given path.
    /// </summary>
    /// <param name="path">Path to assembly file.</param>
    /// <returns></returns>
    public static AssemblyVersionInfo GetVersionInfo(string path)
    {
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(path));
      return new AssemblyVersionInfo(path, 4);
    }
    #endregion  
  }
}