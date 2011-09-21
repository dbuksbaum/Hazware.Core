using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System;

namespace Hazware.Logging
{
  public static class DefaultLogFactories
  {
    public readonly static Func<Type, ILog> NullLoggerFactory =
      (type) => (ILog)Activator.CreateInstance(typeof(NullLogger<>).MakeGenericType(type));
    public readonly static Func<Type, ILog> DebugLoggerFactory =
      (type) => (ILog)Activator.CreateInstance(typeof(DebugLogger<>).MakeGenericType(type));
#if !SILVERLIGHT
    public readonly static Func<Type, ILog> TraceLoggerFactory =
      (type) => (ILog)Activator.CreateInstance(typeof(TraceLogger<>).MakeGenericType(type));
#endif

    public static ILog CreateNullLogger<TClass>()
    {
      return new NullLogger<TClass>();
    }
    public static ILog CreateDebugLogger<TClass>()
    {
      return new DebugLogger<TClass>();
    }
#if !SILVERLIGHT
    public static ILog CreateTraceLogger<TClass>()
    {
      return new TraceLogger<TClass>();
    }
#endif
  }
}