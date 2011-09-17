using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hazware.ComponentModel.Composition.Hosting
{
  /// <summary>
  /// Helper class to configure and build a MEF CompositionContainer.
  /// </summary>
  public sealed class CatalogConfigurator : AbstractDisposable
  {
    #region Fields
    private readonly object _lockObject = new object();
    private readonly AggregateCatalog _currentAggregateCatalog = new AggregateCatalog();
    private CompositionContainer _container;
    #endregion

    #region Chain Methods
    /// <summary>
    /// Chain a delegate to the configuration execution, allowing for the delegate
    /// to be fluently added to the configuration.
    /// </summary>
    /// <param name="configuratorDelegate">Delegate to be chained to the configuration. 
    /// It is safe to pass in null or have a delegate that does nothing.</param>
    /// <returns>The CatalogConfigurator to be used fluently.</returns>
    public CatalogConfigurator Chain(Action<CatalogConfigurator> configuratorDelegate)
    {
      if (configuratorDelegate != null)
        configuratorDelegate(this);
      return this;
    }
    #endregion

    #region AddXXX Methods
    /// <summary>
    /// Adds one or more ComposablePartCatalog instances to the configuration.
    /// </summary>
    /// <param name="catalogs">One or more non-null ComposablePartCatalog instances.</param>
    /// <returns>The CatalogConfigurator to be used fluently.</returns>
    public CatalogConfigurator AddCatalogs(params ComposablePartCatalog[] catalogs)
    {
      foreach (var catalog in catalogs)
        _currentAggregateCatalog.Catalogs.Add(catalog);
      return this;
    }
    /// <summary>
    /// Adds one or more ComposablePartCatalog instances to the configuration.
    /// </summary>
    /// <param name="catalogs">One or more non-null ComposablePartCatalog instances.</param>
    /// <returns>The CatalogConfigurator to be used fluently.</returns>
    public CatalogConfigurator AddCatalogs(IEnumerable<ComposablePartCatalog> catalogs)
    {
      if (catalogs != null)
      {
        foreach (var catalog in catalogs)
          _currentAggregateCatalog.Catalogs.Add(catalog);
      }
      return this;
    }
    /// <summary>
    /// Adds an assembly to the catalogs as an AssemblyCatalog.
    /// </summary>
    /// <param name="assembly">The assembly to be addeded.</param>
    /// <returns>The CatalogConfigurator to be used fluently.</returns>
    public CatalogConfigurator AddAssembly(Assembly assembly)
    {
      return AddCatalogs(new AssemblyCatalog(assembly));
    }
#if !SILVERLIGHT
    /// <summary>
    /// Adds an assembly to the catalogs as an AssemblyCatalog.
    /// </summary>
    /// <param name="codeBase">Code base for the assembly.</param>
    /// <returns>The CatalogConfigurator to be used fluently.</returns>
    public CatalogConfigurator AddAssembly(string codeBase)
    {
      return AddCatalogs(new AssemblyCatalog(codeBase));
    }
#endif
    /// <summary>
    /// Adds one or more assemblies to the catalogs as AssemblyCatalog's.
    /// </summary>
    /// <param name="assemblies">One or more Assembly instances.</param>
    /// <returns>The CatalogConfigurator to be used fluently.</returns>
    public CatalogConfigurator AddAssemblies(params Assembly[] assemblies)
    {
      if (assemblies != null)
      {
        foreach (Assembly assembly in assemblies)
          AddAssembly(assembly);
      }
      return this;
    }
#if !SILVERLIGHT
    /// <summary>
    /// Adds one or more assemblies to the catalogs as AssemblyCatalog's.
    /// </summary>
    /// <param name="codeBases">One or more strings containing the codebase of an assembly.</param>
    /// <returns>The CatalogConfigurator to be used fluently.</returns>
    public CatalogConfigurator AddAssemblies(params string[] codeBases)
    {
      if (codeBases != null)
      {
        foreach (string codeBase in codeBases)
          AddAssembly(codeBase);
      }
      return this;
    }
    /// <summary>
    /// Adds a directory to the catalog as DirectoryCatalog.
    /// <remarks>Will ensure the directory exists.</remarks>
    /// </summary>
    /// <param name="path">The path to a directory that exists or will be created.</param>
    /// <returns>The CatalogConfigurator to be used fluently.</returns>
    public CatalogConfigurator AddDirectory(string path)
    {
      return AddDirectory(path, true);
    }
    /// <summary>
    /// Adds a directory to the catalog as DirectoryCatalog.
    /// </summary>
    /// <param name="path">The path to a directory that exists or will be created.</param>
    /// <param name="ensureExists">true if the directory will be created if it doesnt 
    /// exist, false if it will error if the directory does not exist.</param>
    /// <returns>The CatalogConfigurator to be used fluently.</returns>
    public CatalogConfigurator AddDirectory(string path, bool ensureExists)
    {
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(path));
      CheckForDirectory(path, ensureExists);
      return AddCatalogs(new DirectoryCatalog(path));
    }
    /// <summary>
    /// Adds a directory to the catalog as DirectoryCatalog with the specified search pattern.
    /// <remarks>Will ensure the directory exists.</remarks>
    /// </summary>
    /// <param name="path">The path to a directory that exists or will be created.</param>
    /// <param name="searchPattern">An OS compatable search pattern.</param>
    /// <returns>The CatalogConfigurator to be used fluently.</returns>
    public CatalogConfigurator AddDirectory(string path, string searchPattern)
    {
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(path));
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(searchPattern));
      return AddDirectory(path, searchPattern, true);
    }
    /// <summary>
    /// Adds a directory to the catalog as DirectoryCatalog with the specified search pattern.
    /// </summary>
    /// <param name="path">The path to a directory that exists or will be created.</param>
    /// <param name="searchPattern">An OS compatable search pattern.</param>
    /// <param name="ensureExists">true if the directory will be created if it doesnt 
    /// exist, false if it will error if the directory does not exist.</param>
    /// <returns>The CatalogConfigurator to be used fluently.</returns>
    public CatalogConfigurator AddDirectory(string path, string searchPattern, bool ensureExists)
    {
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(path));
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(searchPattern));
      CheckForDirectory(path, ensureExists);
      return AddCatalogs(new DirectoryCatalog(path, searchPattern));
    }
    /// <summary>
    /// Adds one or more directories to the catalog as DirectoryCatalog's.
    /// <remarks>Will ensure the directory exists.</remarks>
    /// </summary>
    /// <param name="paths">One or more paths to a directory that exists or will be created.</param>
    /// <returns>The CatalogConfigurator to be used fluently.</returns>
    public CatalogConfigurator AddDirectories(params string[] paths)
    {
      Contract.Requires<ArgumentNullException>(paths != null);
      return AddDirectories(true, paths);
    }
    /// <summary>
    /// Adds one or more directories to the catalog as DirectoryCatalog's.
    /// </summary>
    /// <param name="ensureExists">true if the directory will be created if it doesnt 
    /// exist, false if it will error if the directory does not exist.</param>
    /// <param name="paths">One or more paths to a directory that exists or will be created.</param>
    /// <returns>The CatalogConfigurator to be used fluently.</returns>
    public CatalogConfigurator AddDirectories(bool ensureExists, params string[] paths)
    {
      Contract.Requires<ArgumentNullException>(paths != null);
      foreach (string path in paths)
        AddDirectory(path, ensureExists);
      return this;
    }
    /// <summary>
    /// Adds one or more directories to the catalog as DirectoryCatalog's with the 
    /// specified search pattern used for each directory.
    /// <remarks>Will ensure the directory exists.</remarks>
    /// </summary>
    /// <param name="searchPattern">An OS compatable search pattern.</param>
    /// <param name="paths">One or more paths to a directory that exists or will be created.</param>
    /// <returns>The CatalogConfigurator to be used fluently.</returns>
    public CatalogConfigurator AddDirectories(string searchPattern, params string[] paths)
    {
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(searchPattern));
      Contract.Requires<ArgumentNullException>(paths != null);
      return AddDirectories(true, searchPattern, paths);
    }
    /// <summary>
    /// Adds one or more directories to the catalog as DirectoryCatalog's.
    /// </summary>
    /// <param name="ensureExists">true if the directory will be created if it doesnt 
    /// exist, false if it will error if the directory does not exist.</param>
    /// <param name="searchPattern">An OS compatable search pattern.</param>
    /// <param name="paths">One or more paths to a directory that exists or will be created.</param>
    /// <returns>The CatalogConfigurator to be used fluently.</returns>
    public CatalogConfigurator AddDirectories(bool ensureExists, string searchPattern, params string[] paths)
    {
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(searchPattern));
      Contract.Requires<ArgumentNullException>(paths != null);
      Contract.Requires(Contract.ForAll(paths, p => !String.IsNullOrWhiteSpace(p)));
      foreach (string path in paths)
        AddDirectory(path, searchPattern, ensureExists);
      return this;
    }
#endif
    /// <summary>
    /// Adds the collection of types to the configuration.
    /// </summary>
    /// <param name="types">One or more Type instances.</param>
    /// <returns>The CatalogConfigurator to be used fluently.</returns>
    public CatalogConfigurator AddTypes(params Type[] types)
    {
      Contract.Requires<ArgumentNullException>(types != null);
      return AddCatalogs(new TypeCatalog(types));
    }
    /// <summary>
    /// Adds the collection of types to the configuration.
    /// </summary>
    /// <param name="types">One or more Type instances.</param>
    /// <returns>The CatalogConfigurator to be used fluently.</returns>
    public CatalogConfigurator AddTypes(IEnumerable<Type> types)
    {
      Contract.Requires<ArgumentNullException>(types != null);
      return AddCatalogs(new TypeCatalog(types));
    }
    #endregion

    #region BuildContainer
    /// <summary>
    /// Creates the CompositionContainer configured with an aggregate catalog configured fluently.
    /// <remarks>This will return an existing configured container if one exists, otherwise 
    /// it will create it from the configuration.</remarks>
    /// </summary>
    /// <returns>A configured CompositionContainer.</returns>
    public CompositionContainer BuildContainer()
    {
      return BuildContainer(false);
    }
    /// <summary>
    /// Creates the CompositionContainer configured with an aggregate catalog configured fluently.
    /// <remarks>This will return an existing configured container if one exists, otherwise 
    /// it will create it from the configuration.</remarks>
    /// </summary>
    /// <param name="rebuildContainer">true to rebuild the container, false to return the existing configured container.</param>
    /// <returns>A configured CompositionContainer.</returns>
    public CompositionContainer BuildContainer(bool rebuildContainer)
    {
      lock (_lockObject)
      {
        if (rebuildContainer || (_container == null))
          _container = new CompositionContainer(_currentAggregateCatalog);
      }
      return _container;
    }
    #endregion

    #region Private Methods
    private static void CheckForDirectory(string path, bool ensureExists)
    {
      Contract.Requires<ArgumentNullException>(!String.IsNullOrWhiteSpace(path));
      if ((!Directory.Exists(path)) && ensureExists)
      { //  if the directory does not exist, and we need to ensure it does, create it
        Directory.CreateDirectory(path);
      }
    }
    #endregion

    #region Overrides of AbstractDisposable
    /// <summary>
    /// Disposes the unmanaged resources. This is called at the right time in the disposal process.
    /// </summary>
    protected override void DisposeUnmanagedResources()
    {
    }
    /// <summary>
    /// Disposes the managed resources. This is called at the right time in the disposal process.
    /// </summary>
    protected override void DisposeManagedResources()
    {
      _currentAggregateCatalog.Dispose();
      _container.Dispose();
      _container = null;
    }
    #endregion
  }
}
