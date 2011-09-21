using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hazware.Collections.Generic
{
  /// <summary>
  /// Interface for a gerneric provider based on a key
  /// </summary>
  /// <typeparam name="TKey">The type of the key</typeparam>
  /// <typeparam name="TValue">The type of the entity</typeparam>
  public interface IProvider<in TKey, out TValue>
  {
    /// <summary>
    /// Gets an entity based on a key
    /// </summary>
    /// <param name="key">The key</param>
    /// <returns>The entity</returns>
    TValue Get(TKey key);
  }
}
