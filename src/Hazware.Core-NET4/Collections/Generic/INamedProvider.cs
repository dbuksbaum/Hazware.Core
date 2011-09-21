using System.Collections.Generic;
using System.Linq;
using System;

namespace Hazware.Collections.Generic
{
  /// <summary>
  /// A specialized provider that uses a string as the key
  /// </summary>
  /// <typeparam name="TValue">The type of the entity</typeparam>
  public interface INamedProvider<out TValue> : IProvider<string, TValue>
  {
  }
}