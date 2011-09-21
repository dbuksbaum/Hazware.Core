using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System;

namespace Hazware.Logging
{
  ///<summary>
  /// Abstract base class for loggers to add common functionality.
  ///</summary>
  public abstract class AbstractLogger : ILog
  {
    #region Constants
    protected const string LevelDebug = "DEBUG";
    protected const string LevelInfo = "INFO";
    protected const string LevelWarn = "WARN";
    protected const string LevelError = "ERROR";
    protected const string LevelFatal = "FATAL";
    #endregion

    private static readonly FormatMessageHandler DefaultHandler = string.Format;

    #region Implementation of ILog
    ///<summary>
    /// Checks if this logger is enabled for the Debug level.
    ///</summary>
    public abstract bool IsDebugEnabled { get; }
    ///<summary>
    /// Checks if this logger is enabled for the Info level.
    ///</summary>
    public abstract bool IsInfoEnabled { get; }
    ///<summary>
    /// Checks if this logger is enabled for the Warn level.
    ///</summary>
    public abstract bool IsWarnEnabled { get; }
    ///<summary>
    /// Checks if this logger is enabled for the Error level.
    ///</summary>
    public abstract bool IsErrorEnabled { get; }
    ///<summary>
    /// Checks if this logger is enabled for the Fatal level.
    ///</summary>
    public abstract bool IsFatalEnabled { get; }
    ///<summary>
    /// Log a formatabble message with the Debug level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public abstract void Debug(string message, params object[] args);
    ///<summary>
    /// Log a formatabble message with the Debug level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public abstract void Debug(Exception exception, string message, params object[] args);
    ///<summary>
    /// Log a formatabble message with the Debug level.
    ///</summary>
    /// <remarks>
    /// Using this method avoids the cost of creating a message and evaluating message arguments 
    /// that may not be logged due to loglevel settings.
    /// </remarks>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    public void Debug(Func<FormatMessageHandler, string> formatter)
    {
      if (IsDebugEnabled)
        Debug(formatter(DefaultHandler));
    }
    ///<summary>
    /// Log a formatabble message with the Debug level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    public void Debug(Exception exception, Func<FormatMessageHandler, string> formatter)
    {
      if (IsDebugEnabled)
        Debug(exception, formatter(DefaultHandler));
    }
    ///<summary>
    /// Log a formatabble message with the Info level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public abstract void Info(string message, params object[] args);
    ///<summary>
    /// Log a formatabble message with the Info level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public abstract void Info(Exception exception, string message, params object[] args);
    ///<summary>
    /// Log a formatabble message with the Info level.
    ///</summary>
    /// <remarks>
    /// Using this method avoids the cost of creating a message and evaluating message arguments 
    /// that may not be logged due to loglevel settings.
    /// </remarks>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    public void Info(Func<FormatMessageHandler, string> formatter)
    {
      if (IsInfoEnabled)
        Info(formatter(DefaultHandler));
    }
    ///<summary>
    /// Log a formatabble message with the Info level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    public void Info(Exception exception, Func<FormatMessageHandler, string> formatter)
    {
      if (IsInfoEnabled)
        Info(exception, formatter(DefaultHandler));
    }
    ///<summary>
    /// Log a formatabble message with the Warn level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public abstract void Warn(string message, params object[] args);
    ///<summary>
    /// Log a formatabble message with the Warn level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public abstract void Warn(Exception exception, string message, params object[] args);
    public void Warn(Func<FormatMessageHandler, string> formatter)
    {
      if (IsWarnEnabled)
        Warn(formatter(DefaultHandler));
    }
    ///<summary>
    /// Log a formatabble message with the Warn level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    public void Warn(Exception exception, Func<FormatMessageHandler, string> formatter)
    {
      if (IsWarnEnabled)
        Warn(exception, formatter(DefaultHandler));
    }
    ///<summary>
    /// Log a formatabble message with the Error level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public abstract void Error(string message, params object[] args);
    ///<summary>
    /// Log a formatabble message with the Error level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public abstract void Error(Exception exception, string message, params object[] args);
    ///<summary>
    /// Log a formatabble message with the Error level.
    ///</summary>
    /// <remarks>
    /// Using this method avoids the cost of creating a message and evaluating message arguments 
    /// that may not be logged due to loglevel settings.
    /// </remarks>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    public void Error(Func<FormatMessageHandler, string> formatter)
    {
      if (IsErrorEnabled)
        Error(formatter(DefaultHandler));
    }
    ///<summary>
    /// Log a formatabble message with the Error level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    public void Error(Exception exception, Func<FormatMessageHandler, string> formatter)
    {
      if (IsErrorEnabled)
        Error(exception, formatter(DefaultHandler));
    }
    ///<summary>
    /// Log a formatabble message with the Fatal level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public abstract void Fatal(string message, params object[] args);
    ///<summary>
    /// Log a formatabble message with the Fatal level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public abstract void Fatal(Exception exception, string message, params object[] args);
    ///<summary>
    /// Log a formatabble message with the Fatal level.
    ///</summary>
    /// <remarks>
    /// Using this method avoids the cost of creating a message and evaluating message arguments 
    /// that may not be logged due to loglevel settings.
    /// </remarks>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    public void Fatal(Func<FormatMessageHandler, string> formatter)
    {
      if (IsFatalEnabled)
        Fatal(formatter(DefaultHandler));
    }
    ///<summary>
    /// Log a formatabble message with the Fatal level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    public void Fatal(Exception exception, Func<FormatMessageHandler, string> formatter)
    {
      if (IsFatalEnabled)
        Fatal(exception, formatter(DefaultHandler));
    }
    #endregion
  }
}