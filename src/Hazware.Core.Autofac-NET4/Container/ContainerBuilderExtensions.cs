using System.Diagnostics.Contracts;
using Autofac;

namespace Hazware.Container
{
  public static class ContainerBuilderExtensions
  {
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
  }
}