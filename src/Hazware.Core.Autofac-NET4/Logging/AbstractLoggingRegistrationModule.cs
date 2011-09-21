using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Autofac;
using Autofac.Core;

namespace Hazware.Logging
{
  public abstract class AbstractLoggingRegistrationModule : Module
  {
    public const string MefExportTag = "HL_Logging";

    #region Fields
    private readonly ILogProvider _provider;
    #endregion

    #region Constructors
    protected AbstractLoggingRegistrationModule(Type logWrapper)
      : this(type =>
        (ILog)Activator.CreateInstance(logWrapper.MakeGenericType(type)))
    {
    }
    protected AbstractLoggingRegistrationModule(Func<Type, ILog> creator)
      : this(new LogProvider(creator))
    {
    }
    protected AbstractLoggingRegistrationModule(ILogProvider provider)
    {
      Contract.Requires<ArgumentNullException>(provider != null);
      _provider = provider;
    }
    #endregion

    #region Overridable Methods
    protected virtual void OnBeforeRegistration(ContainerBuilder builder)
    {
    }
    protected virtual void OnAfterRegistration(ContainerBuilder builder)
    {
    }
    #endregion

    #region Overrides
    protected override void Load(ContainerBuilder builder)
    {
      OnBeforeRegistration(builder);

      //  register our log provider
      builder.Register(c => _provider).As<ILogProvider>();

      OnAfterRegistration(builder);
    }
    protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
    {
      var type = registration.Activator.LimitType;  //  AF2.0+
      //  .Descriptor.BestKnownImplementationType;  //  AF1.2+

      //  we hook preparing to inject the logger into the constructor
      registration.Preparing += OnComponentPreparing;

      // build the list of actions for type and assign loggers to properties
      var injectors = BuildLogPropertyInjectors(type);

      // no logger properties, no need to hook up the event
// ReSharper disable PossibleMultipleEnumeration
      if (!injectors.Any())
// ReSharper restore PossibleMultipleEnumeration
        return;

      // we hook acticating to inject the logger into the known public properties
      registration.Activating += (s, e) =>
      {
// ReSharper disable PossibleMultipleEnumeration
        foreach (var injector in injectors)
// ReSharper restore PossibleMultipleEnumeration
          injector(e.Context, e.Instance);
      };
    }
    #endregion

    #region Private Methods
    private void OnComponentPreparing(object sender, PreparingEventArgs e)
    {
      Contract.Requires<ArgumentNullException>(e != null);
      Contract.Requires<ArgumentNullException>(e.Parameters != null);
      var type = e.Component.Activator.LimitType;  //  AF2.0+
      //  e.Component.Descriptor.BestKnownImplementationType;  //  AF1.2+
      e.Parameters = e.Parameters.Union(new[]
      {
        new ResolvedParameter((p, i) => (p.ParameterType == typeof(ILog)), 
          (p, i) => e.Context.Resolve<ILogProvider>().Get(type))
      });
    }

    private IEnumerable<Action<IComponentContext, object>> BuildLogPropertyInjectors(Type type)
    {
      // Look for settable properties of type "ILog"
      var loggerProperties = type
        .GetProperties(System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
        .Select(p => new
        {
          PropertyInfo = p,
          p.PropertyType,
          IndexParameters = p.GetIndexParameters(),
          Accessors = p.GetAccessors(false)
        })
        .Where(x => x.PropertyType == typeof(ILog)) // must be an ILog
        .Where(x => x.IndexParameters.Count() == 0) // must not be an indexer
        // must have get/set, or only set
        .Where(x => (x.Accessors.Length != 1) || (x.Accessors[0].ReturnType == typeof(void)));

      // return list of Action<>'s that create and assign the logger to the property
      return loggerProperties.Select(entry => entry.PropertyInfo).Select(pinfo => (Action<IComponentContext, object>)((ctx, instance) =>
                                                                                             {
                                                                                               var pvalue = ctx.Resolve<ILogProvider>().Get(type);
                                                                                               pinfo.SetValue(instance, pvalue, null);
                                                                                             }));
    }
    #endregion
  }
}
