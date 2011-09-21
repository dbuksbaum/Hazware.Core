using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogManager = log4net.LogManager;
using ILogger = log4net.ILog;

// ReSharper disable CheckNamespace
namespace Hazware.Logging.log4net
// ReSharper restore CheckNamespace
{
  internal class Log4NetLogger<T> : AbstractLogger
  {
    #region Fields
    private readonly ILogger _log = LogManager.GetLogger(typeof(T));
    #endregion

    #region ILog<T> Members
    ///<summary>
    /// Checks if this logger is enabled for the Debug level.
    ///</summary>
    public override bool IsDebugEnabled
    {
      get { return _log.IsDebugEnabled; }
    }
    ///<summary>
    /// Checks if this logger is enabled for the Info level.
    ///</summary>
    public override bool IsInfoEnabled
    {
      get { return _log.IsInfoEnabled; }
    }
    ///<summary>
    /// Checks if this logger is enabled for the Warn level.
    ///</summary>
    public override bool IsWarnEnabled
    {
      get { return _log.IsWarnEnabled; }
    }
    ///<summary>
    /// Checks if this logger is enabled for the Error level.
    ///</summary>
    public override bool IsErrorEnabled
    {
      get { return _log.IsErrorEnabled; }
    }
    ///<summary>
    /// Checks if this logger is enabled for the Fatal level.
    ///</summary>
    public override bool IsFatalEnabled
    {
      get { return _log.IsFatalEnabled; }
    }
    ///<summary>
    /// Log a formatabble message with the Debug level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public override void Debug(string message, params object[] args)
    {
      _log.Debug(string.Format(message, args));
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
      _log.Debug(string.Format(message, args), exception);
    }
    ///<summary>
    /// Log a formatabble message with the Info level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public override void Info(string message, params object[] args)
    {
      _log.Info(string.Format(message, args));
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
      _log.Info(string.Format(message, args), exception);
    }
    ///<summary>
    /// Log a formatabble message with the Warn level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public override void Warn(string message, params object[] args)
    {
      _log.Warn(string.Format(message, args));
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
      _log.Warn(string.Format(message, args), exception);
    }
    ///<summary>
    /// Log a formatabble message with the Error level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public override void Error(string message, params object[] args)
    {
      _log.Error(string.Format(message, args));
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
      _log.Error(string.Format(message, args), exception);
    }
    ///<summary>
    /// Log a formatabble message with the Fatal level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public override void Fatal(string message, params object[] args)
    {
      _log.Fatal(string.Format(message, args));
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
      _log.Fatal(string.Format(message, args), exception);
    }
    #endregion
  }
}
