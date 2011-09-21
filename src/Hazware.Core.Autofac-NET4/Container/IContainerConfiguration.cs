
using Hazware.Logging;

namespace Hazware.Container
{
  public interface IContainerConfiguration
  {
    bool ResolveAnything { get; set; }
    ILogProvider LogProvider { get; set; }
  }
}