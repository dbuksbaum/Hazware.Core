using System;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace Hazware.Extensions
{
  /// <summary>
  /// Extension methods for the String class
  /// </summary>
  public static class StringExtensions
  {
    /// <summary>
    /// Returns the length of the string, in a null safe manner.
    /// </summary>
    /// <param name="text">The string to return the length of.</param>
    /// <returns>The length of the string, 0 if null</returns>
    public static int SafeLength(this string text)
    {
      return (String.IsNullOrEmpty(text) ? 0 : text.Length);
    }
    /// <summary>
    /// Formats a string using the source format, provider, and args.
    /// </summary>
    /// <param name="format">The format string to base the returned string on.</param>
    /// <param name="provider">The format provider to use.</param>
    /// <param name="args">The arguments to be used by the format provider.</param>
    /// <returns></returns>
    public static string FormatWith(this string format, IFormatProvider provider, params object[] args)
    {
      Contract.Requires<ArgumentNullException>(format != null);
      Contract.Requires<ArgumentNullException>(provider != null);
      Contract.Requires<ArgumentNullException>(args != null);
      return String.Format(provider, format, args);
    }
    /// <summary>
    /// Formats a string using the source format, args, and invariant culture format provider
    /// </summary>
    /// <param name="format">The format string to base the returned string on.</param>
    /// <param name="args">The arguments to be used by the format provider.</param>
    /// <returns></returns>
    public static string FormatWith(this string format, params object[] args)
    {
      Contract.Requires<ArgumentNullException>(format != null);
      Contract.Requires<ArgumentNullException>(args != null);
      return format.FormatWith(CultureInfo.InvariantCulture, args);
    }
  }
}
