using System.Collections.Generic;
using System.Linq;
using System;

namespace Hazware.Logging
{
  ///<summary>
  /// Logger that discards all log output.
  ///</summary>
  ///<typeparam name="T">The type associated with this logger.</typeparam>
  public sealed class NullLogger<T> : AbstractLogger
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
      get { return false; }
    }
    ///<summary>
    /// Checks if this logger is enabled for the Info level.
    ///</summary>
    public override bool IsInfoEnabled
    {
      get { return false; }
    }
    ///<summary>
    /// Checks if this logger is enabled for the Warn level.
    ///</summary>
    public override bool IsWarnEnabled
    {
      get { return false; }
    }
    ///<summary>
    /// Checks if this logger is enabled for the Error level.
    ///</summary>
    public override bool IsErrorEnabled
    {
      get { return false; }
    }
    ///<summary>
    /// Checks if this logger is enabled for the Fatal level.
    ///</summary>
    public override bool IsFatalEnabled
    {
      get { return false; }
    }
    ///<summary>
    /// Log a formatabble message with the Debug level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public override void Debug(string message, params object[] args)
    {
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
    }
    ///<summary>
    /// Log a formatabble message with the Info level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public override void Info(string message, params object[] args)
    {
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
    }
    ///<summary>
    /// Log a formatabble message with the Warn level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public override void Warn(string message, params object[] args)
    {
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
    }
    ///<summary>
    /// Log a formatabble message with the Error level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public override void Error(string message, params object[] args)
    {
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
    }
    ///<summary>
    /// Log a formatabble message with the Fatal level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public override void Fatal(string message, params object[] args)
    {
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
    }
    #endregion
  }
}