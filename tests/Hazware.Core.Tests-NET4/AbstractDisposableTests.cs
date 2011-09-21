using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

// ReSharper disable CheckNamespace
namespace Hazware.Tests
// ReSharper restore CheckNamespace
{
  [TestFixture, Category("Disposal")]
  public class AbstractDisposableTests
  {
    #region TestClass
    private class TestClass : AbstractDisposable
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
    #endregion

    [Test]
    public void IsDerivedFromIDisposable()
    {
      var myObject = new TestClass();
      myObject.Should().NotBeNull();
      myObject.Should().BeAssignableTo<IDisposable>();
    }
    [Test]
    public void IsDerivedFromIDisposable2()
    {
      var myObject = new TestClass();
      myObject.Should().NotBeNull();
      myObject.Should().BeAssignableTo<IDisposable2>();
    }
    [Test]
    public void IsDerivedFromAbstractDisposable()
    {
      var myObject = new TestClass();
      myObject.Should().NotBeNull();
      myObject.Should().BeAssignableTo<AbstractDisposable>();
    }
    [Test]
    public void IsDisposedIsFalseByDefault()
    {
      var myObject = new TestClass();
      myObject.Should().NotBeNull();
      myObject.IsDisposed.Should().BeFalse();
      //Assert.False(myObject.IsDisposed);
    }
    [Test]
    public void CanDisposeExplictly()
    {
      var myObject = new TestClass();
      myObject.Should().NotBeNull();
      myObject.Dispose();
    }
    [Test]
    public void CanDisposeWithUsings()
    {
      using(var myObject = new TestClass())
      {
        myObject.Should().NotBeNull();
      }
    }
    [Test]
    public void IsDisposedReflectsState()
    {
      var myObject = new TestClass();
      myObject.Should().NotBeNull();
      myObject.IsDisposed.Should().BeFalse();
      myObject.Dispose();
      myObject.IsDisposed.Should().BeTrue();
    }
    [Test]
    public void DisposesManagedResources()
    {
      bool disposedManagedResources = false;
      using(var myObject = new TestClass() {CalledWhenManagedResourcesDisposed = () => disposedManagedResources = true})
      {
        disposedManagedResources.Should().BeFalse();
        myObject.Should().NotBeNull();
      }
      disposedManagedResources.Should().BeTrue();
    }
    [Test]
    public void DisposesUnmanagedResources()
    {
      bool disposedUnmanagedResources = false;
      using(
        var myObject = new TestClass() {CalledWhenUnmanagedResourcesDisposed = () => disposedUnmanagedResources = true})
      {
        disposedUnmanagedResources.Should().BeFalse();
        myObject.Should().NotBeNull();
      }
      disposedUnmanagedResources.Should().BeTrue();
    }
    [Test]
    public void DisposesManagedAndUnmanagedResources()
    {
      bool disposedManagedResources = false;
      bool disposedUnmanagedResources = false;
      using(var myObject = new TestClass()
                             {
                               CalledWhenManagedResourcesDisposed = () => disposedManagedResources = true,
                               CalledWhenUnmanagedResourcesDisposed = () => disposedUnmanagedResources = true
                             })
      {
        disposedManagedResources.Should().BeFalse();
        disposedUnmanagedResources.Should().BeFalse();
        myObject.Should().NotBeNull();
      }
      disposedManagedResources.Should().BeTrue();
      disposedUnmanagedResources.Should().BeTrue();
    }
  }
}
