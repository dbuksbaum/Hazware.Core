using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Hazware.Logging
{
  public delegate string FormatMessageHandler(string format, params object[] args);

  ///<summary>
  /// The ILog interface is used to log messages into the logging framework.
  ///</summary>
  [ContractClass(typeof(ILogContract))]
  public interface ILog
  {
    #region Level Checks
    ///<summary>
    /// Checks if this logger is enabled for the Debug level.
    ///</summary>
    bool IsDebugEnabled { get; }
    ///<summary>
    /// Checks if this logger is enabled for the Info level.
    ///</summary>
    bool IsInfoEnabled { get; }
    ///<summary>
    /// Checks if this logger is enabled for the Warn level.
    ///</summary>
    bool IsWarnEnabled { get; }
    ///<summary>
    /// Checks if this logger is enabled for the Error level.
    ///</summary>
    bool IsErrorEnabled { get; }
    ///<summary>
    /// Checks if this logger is enabled for the Fatal level.
    ///</summary>
    bool IsFatalEnabled { get; }
    #endregion

    #region Debug
    ///<summary>
    /// Log a formatabble message with the Debug level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    void Debug(string message, params object[] args);
    ///<summary>
    /// Log a formatabble message with the Debug level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    void Debug(Exception exception, string message, params object[] args);
    ///<summary>
    /// Log a formatabble message with the Debug level.
    ///</summary>
    /// <remarks>
    /// Using this method avoids the cost of creating a message and evaluating message arguments 
    /// that may not be logged due to loglevel settings.
    /// </remarks>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    void Debug(Func<FormatMessageHandler, string> formatter);
    ///<summary>
    /// Log a formatabble message with the Debug level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    void Debug(Exception exception, Func<FormatMessageHandler, string> formatter);
    #endregion

    #region Info
    ///<summary>
    /// Log a formatabble message with the Info level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    void Info(string message, params object[] args);
    ///<summary>
    /// Log a formatabble message with the Info level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    void Info(Exception exception, string message, params object[] args);
    ///<summary>
    /// Log a formatabble message with the Info level.
    ///</summary>
    /// <remarks>
    /// Using this method avoids the cost of creating a message and evaluating message arguments 
    /// that may not be logged due to loglevel settings.
    /// </remarks>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    void Info(Func<FormatMessageHandler, string> formatter);
    ///<summary>
    /// Log a formatabble message with the Info level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    void Info(Exception exception, Func<FormatMessageHandler, string> formatter);
    #endregion

    #region Warn
    ///<summary>
    /// Log a formatabble message with the Warn level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    void Warn(string message, params object[] args);
    ///<summary>
    /// Log a formatabble message with the Warn level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    void Warn(Exception exception, string message, params object[] args);
    ///<summary>
    /// Log a formatabble message with the Warn level.
    ///</summary>
    /// <remarks>
    /// Using this method avoids the cost of creating a message and evaluating message arguments 
    /// that may not be logged due to loglevel settings.
    /// </remarks>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    void Warn(Func<FormatMessageHandler, string> formatter);
    ///<summary>
    /// Log a formatabble message with the Warn level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    void Warn(Exception exception, Func<FormatMessageHandler, string> formatter);
    #endregion

    #region Error
    ///<summary>
    /// Log a formatabble message with the Error level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    void Error(string message, params object[] args);
    ///<summary>
    /// Log a formatabble message with the Error level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    void Error(Exception exception, string message, params object[] args);
    ///<summary>
    /// Log a formatabble message with the Error level.
    ///</summary>
    /// <remarks>
    /// Using this method avoids the cost of creating a message and evaluating message arguments 
    /// that may not be logged due to loglevel settings.
    /// </remarks>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    void Error(Func<FormatMessageHandler, string> formatter);
    ///<summary>
    /// Log a formatabble message with the Error level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    void Error(Exception exception, Func<FormatMessageHandler, string> formatter);
    #endregion

    #region Fatal
    ///<summary>
    /// Log a formatabble message with the Fatal level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    void Fatal(string message, params object[] args);
    ///<summary>
    /// Log a formatabble message with the Fatal level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    void Fatal(Exception exception, string message, params object[] args);
    ///<summary>
    /// Log a formatabble message with the Fatal level.
    ///</summary>
    /// <remarks>
    /// Using this method avoids the cost of creating a message and evaluating message arguments 
    /// that may not be logged due to loglevel settings.
    /// </remarks>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    void Fatal(Func<FormatMessageHandler, string> formatter);
    ///<summary>
    /// Log a formatabble message with the Fatal level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    void Fatal(Exception exception, Func<FormatMessageHandler, string> formatter);
    #endregion
  }

  public interface ILog<T> : ILog
  {
  }
}
