using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System;

namespace Hazware.Logging
{
  using SysDebug = System.Diagnostics.Debug;

  ///<summary>
  /// Logger that outputs to <see cref="System.Diagnostics.Debug"/>.
  ///</summary>
  ///<typeparam name="T">The type associated with this logger.</typeparam>
  public sealed class DebugLogger<T> : AbstractLogger
  {
    #region Fields
    private readonly string _logName = typeof(T).Name;
    #endregion

    #region ILog<T> Members
    ///<summary>
    /// Checks if this logger is enabled for the Debug level.
    ///</summary>
    public override bool IsDebugEnabled
    {
      get { return true; }
    }
    ///<summary>
    /// Checks if this logger is enabled for the Info level.
    ///</summary>
    public override bool IsInfoEnabled
    {
      get { return true; }
    }
    ///<summary>
    /// Checks if this logger is enabled for the Warn level.
    ///</summary>
    public override bool IsWarnEnabled
    {
      get { return true; }
    }
    ///<summary>
    /// Checks if this logger is enabled for the Error level.
    ///</summary>
    public override bool IsErrorEnabled
    {
      get { return true; }
    }
    ///<summary>
    /// Checks if this logger is enabled for the Fatal level.
    ///</summary>
    public override bool IsFatalEnabled
    {
      get { return true; }
    }
    ///<summary>
    /// Log a formatabble message with the Debug level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public override void Debug(string message, params object[] args)
    {
      WriteLine(LevelDebug, message, args);
    }
    ///<summary>
    /// Log a formatabble message with the Debug level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public override void Debug(System.Exception exception, string message, params object[] args)
    {
      WriteLineWithException(LevelDebug, exception, message, args);
    }
    ///<summary>
    /// Log a formatabble message with the Info level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public override void Info(string message, params object[] args)
    {
      WriteLine(LevelInfo, message, args);
    }
    ///<summary>
    /// Log a formatabble message with the Info level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public override void Info(System.Exception exception, string message, params object[] args)
    {
      WriteLineWithException(LevelInfo, exception, message, args);
    }
    ///<summary>
    /// Log a formatabble message with the Warn level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public override void Warn(string message, params object[] args)
    {
      WriteLine(LevelWarn, message, args);
    }
    ///<summary>
    /// Log a formatabble message with the Warn level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public override void Warn(System.Exception exception, string message, params object[] args)
    {
      WriteLineWithException(LevelWarn, exception, message, args);
    }
    ///<summary>
    /// Log a formatabble message with the Error level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public override void Error(string message, params object[] args)
    {
      WriteLine(LevelError, message, args);
    }
    ///<summary>
    /// Log a formatabble message with the Error level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public override void Error(System.Exception exception, string message, params object[] args)
    {
      WriteLineWithException(LevelError, exception, message, args);
    }
    ///<summary>
    /// Log a formatabble message with the Fatal level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public override void Fatal(string message, params object[] args)
    {
      WriteLine(LevelFatal, message, args);
    }
    ///<summary>
    /// Log a formatabble message with the Fatal level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public override void Fatal(System.Exception exception, string message, params object[] args)
    {
      WriteLineWithException(LevelFatal, exception, message, args);
    }
    #endregion

    #region Private Methods
    private void WriteLine(string level, string message, object[] args)
    {
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(level));
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(message));
      Contract.Requires(args != null);
      SysDebug.WriteLine(string.Format("[{0}] {1}", level, string.Format(message, args)), _logName);
    }
    private void WriteLineWithException(string level, Exception ex, string message, object[] args)
    {
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(level));
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(message));
      Contract.Requires(ex != null);
      Contract.Requires(args != null);
      SysDebug.WriteLine(string.Format("[{0}] {1}\n{2}", level, string.Format(message, args), ex.ToString()), _logName);
    }
    #endregion
  }
}