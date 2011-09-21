using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using Autofac;
using Hazware.Extensions;

namespace Hazware
{
  public static class IoC
  {
    #region Fields
    private static IContainer _container;
    #endregion

    #region Properties
    public static bool IsInitialized
    {
      get { return _container != null; }
    }
    public static IContainer Container
    {
      get { return _container; }
    }
    #endregion

    #region Initialization / Reset
    /// <summary>
    /// Initialize the container with a delegate that creates the container
    /// </summary>
    /// <param name="containerCreator">Delegate that returns a created container</param>
    public static void Initialize(Func<IContainer> containerCreator)
    {
      Contract.Requires<ArgumentNullException>(containerCreator != null);
      Initialize(containerCreator());
    }
    /// <summary>
    /// Initializes the IoC using the specified dependency resolver.
    /// </summary>
    /// <param name="container">The container.</param>
    public static void Initialize(IContainer container)
    {
      Contract.Requires<ArgumentNullException>(container != null);
      Contract.Requires<InvalidOperationException>(!IsInitialized, "Cannot initialize when already initialized.");
      _container = container;
    }
    /// <summary>
    /// Resets the IoC by clearing the current dependency resolver.
    /// </summary>
    public static void Reset()
    {
      try
      {
        if (_container != null)
          _container.Dispose();
      }
      catch(Exception ex)
      {
        Debug.WriteLine("Exception disposing of container. MSG={0}", ex.Message);
      }
      finally
      {
        _container = null;
      }
    }
    #endregion

    #region Resolution
    #region Resolve
    /// <summary>
    /// Resolve a service by type
    /// </summary>
    /// <typeparam name="T">Type of the service to locate</typeparam>
    /// <returns>The service</returns>
    public static T Resolve<T>()
    {
      Contract.Requires<InvalidOperationException>(IsInitialized, "Container is not initialized.");
      return Container.Resolve<T>();
    }
    /// <summary>
    /// Resolve a service by type and name
    /// </summary>
    /// <typeparam name="T">Type of the service to locate</typeparam>
    /// <param name="name">The name of the service</param>
    /// <returns>The service</returns>
    public static T Resolve<T>(string name)
    {
      Contract.Requires<InvalidOperationException>(IsInitialized, "Container is not initialized.");
      return Container.ResolveNamed<T>(name);
    }
    #endregion
    #region TryResolve
    /// <summary>
    /// Tries to resolve the component by type, but return 
    /// null instead of throwing if it is not there.
    /// Useful for optional dependencies.
    /// </summary>
    /// <typeparam name="T">The type of service to return</typeparam>
    /// <returns>The service or null</returns>
    public static T TryResolve<T>()
    {
      Contract.Requires<InvalidOperationException>(IsInitialized, "Container is not initialized.");
      return Container.TryResolve<T>();
    }
    /// <summary>
    /// Tries to resolve the component by type and name, but 
    /// will return null instead of throwing if it is not there.
    /// Useful for optional dependencies.
    /// </summary>
    /// <typeparam name="T">The type of service to return</typeparam>
    /// <param name="name">The name of the service to return</param>
    /// <returns>The service or null</returns>
    public static T TryResolve<T>(string name)
    {
      Contract.Requires<InvalidOperationException>(IsInitialized, "Container is not initialized.");
      return Container.TryResolve<T>(name);
    }
    /// <summary>
    /// Tries to resolve the compoennt by type, but will return 
    /// the default value instead of throwing if it could not 
    /// be resolved.
    /// Useful for optional dependencies.
    /// </summary>
    /// <typeparam name="T">The type of service to return</typeparam>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The service or default</returns>
    public static T TryResolve<T>(T defaultValue)
    {
      Contract.Requires<InvalidOperationException>(IsInitialized, "Container is not initialized.");
      //  we add the argument specification in case T is a string
      //  and the method resolution things it is tyring to do
      //  TryResolve<T>(name)
// ReSharper disable RedundantArgumentName
      return Container.TryResolve<T>(defaultValue: defaultValue);
// ReSharper restore RedundantArgumentName
    }
    /// <summary>
    /// Tries to resolve the compoennt by type and name, but 
    /// will return the default value instead of throwing if 
    /// it could not be resolved.
    /// Useful for optional dependencies.
    /// </summary>
    /// <typeparam name="T">The type of service to return</typeparam>
    /// <param name="name">The name of the service to return</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>The service or default</returns>
    public static T TryResolve<T>(string name, T defaultValue)
      where T : class
    {
      Contract.Requires<InvalidOperationException>(IsInitialized, "Container is not initialized.");
      return Container.TryResolve<T>(name, defaultValue);
    }
    #endregion
    #region ResolveAll
    /// <summary>
    /// Resolves all services of requested type
    /// </summary>
    /// <typeparam name="T">Type of the service to locate</typeparam>
    /// <returns>Enumeration of services located</returns>
    public static IEnumerable<T> ResolveAll<T>()
    {
      Contract.Requires<InvalidOperationException>(IsInitialized, "Container is not initialized.");
      return TryResolve<IEnumerable<T>>(new T[] { });
    }
    #endregion
    #endregion    
  }
}
