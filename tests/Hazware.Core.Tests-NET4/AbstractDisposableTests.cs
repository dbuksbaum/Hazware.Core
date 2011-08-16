using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Hazware.Core.Tests_NET4
{
  [TestFixture]
  public class AbstractDisposableTests
  {
    class TestClass : AbstractDisposable
    {
      public Action CalledWhenUnmanagedResourcesDisposed = () => { };
      public Action CalledWhenManagedResourcesDisposed = () => { };
      
      #region Overrides of AbstractDisposable
      /// <summary>
      /// Disposes the unmanaged resources. This is called at the right time in the disposal process.
      /// </summary>
      protected override void DisposeUnmanagedResources()
      {
        CalledWhenUnmanagedResourcesDisposed();
      }
      /// <summary>
      /// Disposes the managed resources. This is called at the right time in the disposal process.
      /// </summary>
      protected override void DisposeManagedResources()
      {
        CalledWhenManagedResourcesDisposed();
      }
      #endregion
    }

    [Test]
    public void IsDerivedFromIDisposable()
    {
      var myObject = new TestClass();
      Assert.IsNotNull(myObject);
      Assert.IsInstanceOf<IDisposable>(myObject);
    }
    [Test]
    public void IsDerivedFromIDisposable2()
    {
      var myObject = new TestClass();
      Assert.IsNotNull(myObject);
      Assert.IsInstanceOf<IDisposable2>(myObject);
    }
    [Test]
    public void IsDerivedFromAbstractDisposable()
    {
      var myObject = new TestClass();
      Assert.IsNotNull(myObject);
      Assert.IsInstanceOf<AbstractDisposable>(myObject);
    }
    [Test]
    public void IsDisposedIsFalseByDefault()
    {
      var myObject = new TestClass();
      Assert.IsNotNull(myObject);
      Assert.IsFalse(myObject.IsDisposed);
    }
    [Test]
    public void CanDisposeExplictly()
    {
      var myObject = new TestClass();
      Assert.IsNotNull(myObject);
      myObject.Dispose();
    }
    [Test]
    public void CanDisposeWithUsings()
    {
      using (var myObject = new TestClass())
      {
        Assert.IsNotNull(myObject);
      }
    }
    [Test]
    public void IsDisposedReflectsState()
    {
      var myObject = new TestClass();
      Assert.IsNotNull(myObject);
      Assert.IsFalse(myObject.IsDisposed);
      myObject.Dispose();
      Assert.IsTrue(myObject.IsDisposed);
    }
    [Test]
    public void DisposesManagedResources()
    {
      bool disposedManagedResources = false;
      using (var myObject = new TestClass() { CalledWhenManagedResourcesDisposed = () => disposedManagedResources = true })
      {
        Assert.IsFalse(disposedManagedResources);
        Assert.IsNotNull(myObject);
      }
      Assert.IsTrue(disposedManagedResources);
    }
    [Test]
    public void DisposesUnmanagedResources()
    {
      bool disposedUnmanagedResources = false;
      using (var myObject = new TestClass() { CalledWhenUnmanagedResourcesDisposed = () => disposedUnmanagedResources = true })
      {
        Assert.IsFalse(disposedUnmanagedResources);
        Assert.IsNotNull(myObject);
      }
      Assert.IsTrue(disposedUnmanagedResources);
    }
    [Test]
    public void DisposesManagedAndUnmanagedResources()
    {
      bool disposedManagedResources = false;
      bool disposedUnmanagedResources = false;
      using (var myObject = new TestClass()
                              {
                                CalledWhenManagedResourcesDisposed = () => disposedManagedResources = true, 
                                CalledWhenUnmanagedResourcesDisposed = () => disposedUnmanagedResources = true 
                              })
      {
        Assert.IsFalse(disposedManagedResources);
        Assert.IsFalse(disposedUnmanagedResources);
        Assert.IsNotNull(myObject);
      }
      Assert.IsTrue(disposedManagedResources);
      Assert.IsTrue(disposedUnmanagedResources);
    }
  }
}
