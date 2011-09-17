using System.Diagnostics.Contracts;
using System.Reflection;
using Autofac;
using Autofac.Core;

namespace Hazware.Container
{
  /// <summary>
  /// TODO:
  ///   * Add Default Logging
  ///   * Add Support for Unregistered Resolution
  ///   * Add Support auto-scanning
  /// </summary>
  public static class ContainerCreators
  {
    public static IContainer Create(IModule[] modules, IContainerConfiguration configuration)
    {
      Contract.Requires(modules != null);
      Contract.Requires(modules.Length > 0);
      Contract.Requires(Contract.ForAll(modules, m => m != null));

      var builder = new ContainerBuilder();
      builder.Configure(configuration);

      foreach (var module in modules)
      {
        builder.RegisterModule(module);
      }
      return builder.Build();
    }

    public static IContainer Create(Assembly[] assemblies, IContainerConfiguration configuration)
    {
      Contract.Requires(assemblies != null);

      var builder = new ContainerBuilder();
      builder.Configure(configuration);
      builder.RegisterAssemblyTypes(assemblies);
      return builder.Build();
    }
  }
}