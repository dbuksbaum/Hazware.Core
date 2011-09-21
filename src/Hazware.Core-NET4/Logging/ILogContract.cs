using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System;

namespace Hazware.Logging
{
  [ContractClassFor(typeof(ILog))]
// ReSharper disable InconsistentNaming
  abstract class ILogContract : ILog
// ReSharper restore InconsistentNaming
  {
    #region Implementation of ILog
    ///<summary>
    /// Checks if this logger is enabled for the Debug level.
    ///</summary>
    public bool IsDebugEnabled
    {
      get { return false; }
    }
    ///<summary>
    /// Checks if this logger is enabled for the Info level.
    ///</summary>
    public bool IsInfoEnabled
    {
      get { return false; }
    }
    ///<summary>
    /// Checks if this logger is enabled for the Warn level.
    ///</summary>
    public bool IsWarnEnabled
    {
      get { return false; }
    }
    ///<summary>
    /// Checks if this logger is enabled for the Error level.
    ///</summary>
    public bool IsErrorEnabled
    {
      get { return false; }
    }
    ///<summary>
    /// Checks if this logger is enabled for the Fatal level.
    ///</summary>
    public bool IsFatalEnabled
    {
      get { return false; }
    }
    ///<summary>
    /// Log a formatabble message with the Debug level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public void Debug(string message, params object[] args)
    {
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(message));
      Contract.Requires<ArgumentNullException>(args != null);
    }
    ///<summary>
    /// Log a formatabble message with the Debug level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public void Debug(Exception exception, string message, params object[] args)
    {
      Contract.Requires<ArgumentNullException>(exception != null);
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(message));
      Contract.Requires<ArgumentNullException>(args != null);
    }
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
      Contract.Requires<ArgumentNullException>(formatter != null);
    }
    ///<summary>
    /// Log a formatabble message with the Debug level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    public void Debug(Exception exception, Func<FormatMessageHandler, string> formatter)
    {
      Contract.Requires<ArgumentNullException>(exception != null);
      Contract.Requires<ArgumentNullException>(formatter != null);
    }
    ///<summary>
    /// Log a formatabble message with the Info level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public void Info(string message, params object[] args)
    {
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(message));
      Contract.Requires<ArgumentNullException>(args != null);
    }
    ///<summary>
    /// Log a formatabble message with the Info level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public void Info(Exception exception, string message, params object[] args)
    {
      Contract.Requires<ArgumentNullException>(exception != null);
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(message));
      Contract.Requires<ArgumentNullException>(args != null);
    }
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
      Contract.Requires<ArgumentNullException>(formatter != null);
    }
    ///<summary>
    /// Log a formatabble message with the Info level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    public void Info(Exception exception, Func<FormatMessageHandler, string> formatter)
    {
      Contract.Requires<ArgumentNullException>(exception != null);
      Contract.Requires<ArgumentNullException>(formatter != null);
    }
    ///<summary>
    /// Log a formatabble message with the Warn level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public void Warn(string message, params object[] args)
    {
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(message));
      Contract.Requires<ArgumentNullException>(args != null);
    }
    ///<summary>
    /// Log a formatabble message with the Warn level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public void Warn(Exception exception, string message, params object[] args)
    {
      Contract.Requires<ArgumentNullException>(exception != null);
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(message));
      Contract.Requires<ArgumentNullException>(args != null);
    }
    ///<summary>
    /// Log a formatabble message with the Warn level.
    ///</summary>
    /// <remarks>
    /// Using this method avoids the cost of creating a message and evaluating message arguments 
    /// that may not be logged due to loglevel settings.
    /// </remarks>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    public void Warn(Func<FormatMessageHandler, string> formatter)
    {
      Contract.Requires<ArgumentNullException>(formatter != null);
    }
    ///<summary>
    /// Log a formatabble message with the Warn level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    public void Warn(Exception exception, Func<FormatMessageHandler, string> formatter)
    {
      Contract.Requires<ArgumentNullException>(exception != null);
      Contract.Requires<ArgumentNullException>(formatter != null);
    }
    ///<summary>
    /// Log a formatabble message with the Error level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public void Error(string message, params object[] args)
    {
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(message));
      Contract.Requires<ArgumentNullException>(args != null);
    }
    ///<summary>
    /// Log a formatabble message with the Error level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public void Error(Exception exception, string message, params object[] args)
    {
      Contract.Requires<ArgumentNullException>(exception != null);
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(message));
      Contract.Requires<ArgumentNullException>(args != null);
    }
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
      Contract.Requires<ArgumentNullException>(formatter != null);
    }
    ///<summary>
    /// Log a formatabble message with the Error level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    public void Error(Exception exception, Func<FormatMessageHandler, string> formatter)
    {
      Contract.Requires<ArgumentNullException>(exception != null);
      Contract.Requires<ArgumentNullException>(formatter != null);
    }
    ///<summary>
    /// Log a formatabble message with the Fatal level.
    ///</summary>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public void Fatal(string message, params object[] args)
    {
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(message));
      Contract.Requires<ArgumentNullException>(args != null);
    }
    ///<summary>
    /// Log a formatabble message with the Fatal level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="message">String containing zero or more format items</param>
    ///<param name="args">Object array containing zero or more objects to format</param>
    public void Fatal(Exception exception, string message, params object[] args)
    {
      Contract.Requires<ArgumentNullException>(exception != null);
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(message));
      Contract.Requires<ArgumentNullException>(args != null);
    }
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
      Contract.Requires<ArgumentNullException>(formatter != null);
    }
    ///<summary>
    /// Log a formatabble message with the Fatal level including the stack 
    /// trace of the <see cref="Exception"/> passed as a parameter.
    ///</summary>
    ///<param name="exception">The exception to log, including its stack trace.</param>
    ///<param name="formatter">A callback used by the logger to obtain the message if log level is matched</param>
    public void Fatal(Exception exception, Func<FormatMessageHandler, string> formatter)
    {
      Contract.Requires<ArgumentNullException>(exception != null);
      Contract.Requires<ArgumentNullException>(formatter != null);
    }
    #endregion
  }
}