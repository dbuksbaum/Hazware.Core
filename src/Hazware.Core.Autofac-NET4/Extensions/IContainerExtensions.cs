using Autofac;
using Autofac.Core;

namespace Hazware.Extensions
{
  /// <summary>
  /// Extensions for the Autofac <see cref="IContainer"/>.
  /// </summary>
  // ReSharper disable InconsistentNaming
  public static class IContainerExtensions
  // ReSharper restore InconsistentNaming
  {
    /// <summary>
    /// Try to retrieve a service from the context.
    /// </summary>
    /// <param name="container">The container from which to resolve the service.</param>
    /// <param name="defaultValue">Instance returned if the type is not found.</param>
    /// <typeparam name="T">The type of the service to resolve.</typeparam>
    /// <exception cref="DependencyResolutionException"/>    /// 
    /// <returns>The resulting component instance providing the service, or defaultValue.</returns>
    public static T TryResolve<T>(this IContainer container, T defaultValue = default(T))
    {
      object instance;
      if (container.TryResolve(typeof(T), out instance))
        return (T)instance;
      return defaultValue;
    }
    /// <summary>
    /// Try to retrieve a service from the context by name.
    /// </summary>
    /// <param name="container">The container from which to resolve the service.</param>
    /// <param name="name">The name of the service to resolve.</param>
    /// <param name="defaultValue">Instance returned if the type is not found.</param>
    /// <typeparam name="T">The type of the service to resolve.</typeparam>
    /// <exception cref="DependencyResolutionException"/>    /// 
    /// <returns>The resulting component instance providing the service, or defaultValue.</returns>
    public static T TryResolve<T>(this IContainer container, string name, T defaultValue = default(T))
    {
      object instance;
      if (container.TryResolveNamed(name, typeof(T), out instance))
        return (T)instance;
      return defaultValue;
    }
  }
}
