using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Builder;
using Autofac.Core;

namespace Hazware.Container
{
  /// <summary>
  /// Provides a registration source for Autofac that will resolve concrete
  /// types that have not been previously registered.
  /// </summary>
  internal sealed class ResolveAnythingSource : IRegistrationSource
  {
    #region IRegistrationSource Members
    /// <summary>
    /// Retrieve registrations for an unregistered service, to be used
    ///             by the container.
    /// </summary>
    /// <param name="service">The service that was requested.</param><param name="registrationAccessor">A function that will return existing registrations for a service.</param>
    /// <returns>
    /// Registrations providing the service.
    /// </returns>
    /// <remarks>
    /// If the source is queried for service s, and it returns a component that implements both s and s', then it
    ///             will not be queried again for either s or s'. This means that if the source can return other implementations
    ///             of s', it should return these, plus the transitive closure of other components implementing their 
    ///             additional services, along with the implementation of s. It is not an error to return components
    ///             that do not implement <paramref name="service"/>.
    /// </remarks>
    public IEnumerable<IComponentRegistration> RegistrationsFor(
      Service service,
      Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
    {
      var ts = service as TypedService;
      if (ts != null && !ts.ServiceType.IsAbstract && ts.ServiceType.IsClass)
      {
        var rb = RegistrationBuilder.ForType(ts.ServiceType);
        return new[] { rb.CreateRegistration() };
      }

      return Enumerable.Empty<IComponentRegistration>();
    }

    /// <summary>
    /// Gets whether the registrations provided by this source are 1:1 adapters on top
    ///             of other components (I.e. like Meta, Func or Owned.)
    /// </summary>
    public bool IsAdapterForIndividualComponents
    {
      get { return false; }
    }
    #endregion
  }
}
