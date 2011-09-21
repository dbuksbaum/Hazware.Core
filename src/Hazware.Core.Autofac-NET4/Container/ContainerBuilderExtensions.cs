using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Autofac;
using Hazware.Logging;
using Hazware.Extensions;

namespace Hazware.Container
{
  public static class ContainerBuilderExtensions
  {
    private readonly static Dictionary<WeakReference, bool> LoggersInitialized = new Dictionary<WeakReference, bool>();

    public static ContainerBuilder Configure(this ContainerBuilder builder, IContainerConfiguration configuration)
    {
      Contract.Requires(builder != null);
      
      if (configuration != null)
      { //  we have a configuration, so do something
        if(configuration.ResolveAnything)
          builder.RegisterSource(new ResolveAnythingSource());
      }

      return builder;
    }

    public static ContainerBuilder UseLoggerFactory(this ContainerBuilder builder, Func<Type, ILog> loggerFactory)
    {
      Contract.Requires(builder != null);
      Contract.Requires(loggerFactory != null);

      CheckIfLoggerInitialized(builder);
      builder.RegisterModule(new SimpleLoggingRegistrationModule(loggerFactory));
      LoggersInitialized.Add(new WeakReference(builder), true);
      return builder;
    }
    public static ContainerBuilder UseLogProvider(this ContainerBuilder builder, ILogProvider logProvider)
    {
      Contract.Requires(builder != null);
      Contract.Requires(logProvider != null);

      CheckIfLoggerInitialized(builder);
      builder.RegisterModule(new SimpleLoggingRegistrationModule(logProvider));
      LoggersInitialized.Add(new WeakReference(builder), true);
      return builder;
    }
    public static ContainerBuilder UseNullLogger(this ContainerBuilder builder)
    {
      return UseLoggerFactory(builder, DefaultLogFactories.NullLoggerFactory);
    }
    public static ContainerBuilder UseDebugLogger(this ContainerBuilder builder)
    {
      return UseLoggerFactory(builder, DefaultLogFactories.DebugLoggerFactory);
    }
#if !SILVERLIGHT
    public static ContainerBuilder UseTraceLogger(this ContainerBuilder builder)
    {
      return UseLoggerFactory(builder, DefaultLogFactories.TraceLoggerFactory);
    }
#endif
    private static void CheckIfLoggerInitialized(ContainerBuilder builder)
    {
      //  prune disposed builders
      var disposedBuilders = LoggersInitialized.Where(kv => !kv.Key.IsAlive);
      disposedBuilders.ForEach(db => LoggersInitialized.Remove(db.Key));
      
      //  check if logger initialized for this builder
      if (LoggersInitialized.Any(kv => kv.Key.IsAlive && object.ReferenceEquals(kv.Key.Target, builder)))
      { //  already have an initialized logger
        throw new InvalidOperationException("Logger Already Initialized.");
      }
    }
  }
}