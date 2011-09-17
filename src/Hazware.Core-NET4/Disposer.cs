using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Hazware
{
  /// <summary>
  /// This class provides helper functions for dealing with the proper disposal of
  /// objects that implement the IDisposal pattern.
  /// </summary>
  public static class Disposer
  {
    /// <summary>
    /// Disposes an object. If the object does not implement <see cref="T:System.IDisposable"/> or if val is null, 
    /// then it will not throw an error, but just silently return.
    /// If val is an <see cref="T:System.Collections.IEnumerable"/> then it will call the specialized 
    /// DisposeObject(IEnumerable) method.
    /// </summary>
    /// <param name="val">The object to be disposed.</param>
    public static void DisposeObject(Object val)
    {
      if (val == null)
      {
        return;
      }
      else if (val is IEnumerable)
      {
        DisposeObject(val as IEnumerable);
      }
      else if (val is IDisposable)
      {
        DisposeObject(val as IDisposable);
      }
    }
    /// <summary>
    /// Disposes an object. If is safe to call this method when the value is null. If null, no 
    /// error will be thrown, the method will just silently return.
    /// </summary>
    /// <typeparam name="T">A type derived from <see cref="T:System.IDisposable"/></typeparam>
    /// <param name="val">The object to dispose.</param>
    public static void DisposeObject<T>(T val)
      where T : class, IDisposable
    {
      if (val != null)
      {
        val.Dispose();
      }
    }
    /// <summary>
    /// Disposes all objects within a collection. If is safe to call this method when the collection is null. 
    /// If null, no error will be thrown, the method will just silently return.
    /// It is also safe for elements within the collection to be null or not implement <see cref="T:System.IDisposable"/>.
    /// This will not dispose the collection object itself.
    /// </summary>
    /// <param name="collection">The collection.</param>
    public static void DisposeObject(IEnumerable collection)
    {
      if(collection == null)
      {
        return;
      }
      foreach (var obj in collection)
      {
        DisposeObject(obj);
      }
    }
    /// <summary>
    /// Disposes all values within a dictionary. If is safe to call this method when the dictionary is null. 
    /// If null, no error will be thrown, the method will just silently return.
    /// It is also safe for the values within the dictionary to be null or not implement <see cref="T:System.IDisposable"/>.
    /// This will not dispose the dictionary object itself.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    public static void DisposeObject(IDictionary dictionary)
    {
      if(dictionary == null)
      {
        return;
      }
      foreach (DictionaryEntry entry in dictionary)
      {
        DisposeObject(entry.Value);
      }
    }
    /// <summary>
    /// Disposes all values within a generic dictionary. If is safe to call this method when the dictionary is null. 
    /// If null, no error will be thrown, the method will just silently return.
    /// It is also safe for the values within the dictionary to be null, but they must implement <see cref="T:System.IDisposable"/>.
    /// This will not dispose the dictionary object itself.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value derived from <see cref="T:System.IDisposable"/>.</typeparam>
    /// <param name="dictionary">The dictionary.</param>
    public static void DisposeObject<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
      where TValue : IDisposable
    {
      if(dictionary == null)
      {
        return;
      }
      foreach (var entry in dictionary)
      {
        DisposeObject(entry.Value as object);
      }
    }
  }
}