using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

// ReSharper disable CheckNamespace
namespace Hazware.Autofac.Tests
// ReSharper restore CheckNamespace
{
  public interface ITestClass
  {
    string DoSomething();
  }
  public interface IOtherClass
  {
    string DontDoAnything();
  }

  [TestFixture, Category("Container")]
  public class AutofacTests
  {
    #region Reset
    [SetUp]
    public void Setup()
    {
      IoC.Reset();
    }
    #endregion

    #region Initialization Tests
    [Test]
    public void ContainerIsNull()
    {
      IoC.Container.Should().BeNull();
    }
    [Test]
    public void ContainerIsNotInitialized()
    {
      IoC.IsInitialized.Should().BeFalse();
    }
    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void InitializeWithNullFunctorThrows()
    {
      IoC.Initialize((Func<IContainer>)null);
    }
    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void InitializeWithNullContainerThrows()
    {
      IoC.Initialize((IContainer)null);
    }
    [Test]
    public void InitializeWithContainerWorks()
    {
      var container = A.Fake<IContainer>();
      IoC.IsInitialized.Should().BeFalse();
      IoC.Initialize(container);
      IoC.IsInitialized.Should().BeTrue();
      IoC.Container.Should().NotBeNull();
      IoC.Container.Should().BeSameAs(container);
    }
    [Test]
    public void InitializeWithFunctorWorks()
    {
      var container = A.Fake<IContainer>();
      IoC.IsInitialized.Should().BeFalse();
      IoC.Initialize(() => container);
      IoC.IsInitialized.Should().BeTrue();
      IoC.Container.Should().NotBeNull();
      IoC.Container.Should().BeSameAs(container);
    }
    #endregion

    #region Reset
    [Test]
    public void ResetDoesNotThrowWhenNotInitialized()
    {
      IoC.IsInitialized.Should().BeFalse();
      IoC.Reset();
    }
    [Test]
    public void ResetClearsContainerAndResetsIsInitialized()
    {
      var container = A.Fake<IContainer>();
      IoC.IsInitialized.Should().BeFalse();
      IoC.Initialize(container);
      IoC.IsInitialized.Should().BeTrue();
      IoC.Container.Should().NotBeNull();
      IoC.Container.Should().BeSameAs(container);
      IoC.Reset();
      IoC.IsInitialized.Should().BeFalse();
      IoC.Container.Should().BeNull();
    }
    #endregion

    #region Resolve Tests
    [Test]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ResolveWhenNotInitializedThrows()
    {
      IoC.Resolve<string>();
    }
    [Test]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ResolveNamedWhenNotInitializedThrows()
    {
      IoC.Resolve<string>("Name");
    }
    [Test]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TryResolveWhenNotInitializedThrows()
    {
      IoC.TryResolve<string>();
    }
    [Test]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TryResolveNamedWhenNotInitializedThrows()
    {
      IoC.TryResolve<string>("Name");
    }
    [Test]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TryResolveWithDefaultWhenNotInitializedThrows()
    {
      IoC.TryResolve<string>(string.Empty);
    }
    [Test]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TryResolveNamedWithDefaultWhenNotInitializedThrows()
    {
      IoC.TryResolve<string>("Name", string.Empty);
    }
    [Test]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ResolveAllWhenNotInitializedThrows()
    {
      IoC.ResolveAll<string>();
    }
    [Test]
    public void ResolveWorks()
    {
      IoC.IsInitialized.Should().BeFalse();
      var cb = new ContainerBuilder();
      cb.Register(c => A.Fake<ITestClass>());
      var container = cb.Build();

      IoC.Initialize(container);
      IoC.IsInitialized.Should().BeTrue();
      var testClass = IoC.Resolve<ITestClass>();
      testClass.Should().NotBeNull();
      var testClass2 = IoC.Resolve<ITestClass>();
      testClass2.Should().NotBeNull();
      testClass2.Should().NotBeSameAs(testClass);
    }
    [Test]
    public void ResolveNamedWorks()
    {
      IoC.IsInitialized.Should().BeFalse();
      var cb = new ContainerBuilder();
      const string serviceName = "MyInstance";
      cb.Register(c => A.Fake<ITestClass>()).Named<ITestClass>(serviceName);
      var container = cb.Build();

      IoC.Initialize(container);
      IoC.IsInitialized.Should().BeTrue();
      var testClass = IoC.Resolve<ITestClass>(serviceName);
      testClass.Should().NotBeNull();
      var testClass2 = IoC.Resolve<ITestClass>(serviceName);
      testClass2.Should().NotBeNull();
      testClass2.Should().NotBeSameAs(testClass);
    }
    [Test]
    public void ResolveAllWorks()
    {
      IoC.IsInitialized.Should().BeFalse();
      var cb = new ContainerBuilder();
      const string serviceName = "MyInstance";
      cb.Register(c => A.Fake<ITestClass>());
      cb.Register(c => A.Fake<ITestClass>());
      cb.Register(c => A.Fake<ITestClass>());
      cb.Register(c => A.Fake<ITestClass>());
      var container = cb.Build();

      IoC.Initialize(container);
      IoC.IsInitialized.Should().BeTrue();
      var testClasses = IoC.ResolveAll<ITestClass>();
      testClasses.Should().NotBeNull();
      testClasses.Should().BeAssignableTo<IEnumerable<ITestClass>>();
      //  only named entities
      testClasses.Count().Should().Be(4);
      testClasses.Should().NotContainNulls();
      testClasses.Should().ContainItemsAssignableTo<ITestClass>();
    }
    [Test]
    public void ResolveAllWhenEmptyWorks()
    {
      IoC.IsInitialized.Should().BeFalse();
      var cb = new ContainerBuilder();
      var container = cb.Build();

      IoC.Initialize(container);
      IoC.IsInitialized.Should().BeTrue();
      var testClasses = IoC.ResolveAll<ITestClass>();
      testClasses.Should().NotBeNull();
      testClasses.Should().BeAssignableTo<IEnumerable<ITestClass>>();
      testClasses.Count().Should().Be(0);
      testClasses.Should().BeEmpty();
    }
    [Test]
    public void TryResolveWorks()
    {
      IoC.IsInitialized.Should().BeFalse();
      var cb = new ContainerBuilder();
      cb.Register(c => A.Fake<ITestClass>());
      var container = cb.Build();

      IoC.Initialize(container);
      IoC.IsInitialized.Should().BeTrue();
      var testClass = IoC.TryResolve<ITestClass>();
      testClass.Should().NotBeNull();
      var otherClass = IoC.TryResolve<IOtherClass>();
      otherClass.Should().BeNull();
    }
    [Test]
    public void TryResolveWithDefaultWorks()
    {
      IoC.IsInitialized.Should().BeFalse();
      var defaultTestClass = A.Fake<ITestClass>();
      var defaultOtherClass = A.Fake<IOtherClass>();
      var cb = new ContainerBuilder();
      cb.Register(c => A.Fake<ITestClass>());
      var container = cb.Build();

      IoC.Initialize(container);
      IoC.IsInitialized.Should().BeTrue();
      var testClass = IoC.TryResolve<ITestClass>(defaultTestClass);
      testClass.Should().NotBeNull();
      testClass.Should().NotBeSameAs(defaultTestClass);
      var otherClass = IoC.TryResolve<IOtherClass>(defaultOtherClass);
      otherClass.Should().NotBeNull();
      otherClass.Should().BeSameAs(defaultOtherClass);
    }
    [Test]
    public void TryResolveNamedWorks()
    {
      IoC.IsInitialized.Should().BeFalse();
      var cb = new ContainerBuilder();
      const string serviceName = "MyService";
      cb.Register(c => A.Fake<ITestClass>()).Named<ITestClass>(serviceName);
      var container = cb.Build();

      IoC.Initialize(container);
      IoC.IsInitialized.Should().BeTrue();
      var testClass = IoC.TryResolve<ITestClass>(serviceName);
      testClass.Should().NotBeNull();
      var otherClass = IoC.TryResolve<IOtherClass>(serviceName);
      otherClass.Should().BeNull();
    }
    [Test]
    public void TryResolveNamedWithDefaultWorks()
    {
      IoC.IsInitialized.Should().BeFalse();
      var defaultTestClass = A.Fake<ITestClass>();
      var defaultOtherClass = A.Fake<IOtherClass>();
      var cb = new ContainerBuilder();
      const string serviceName = "MyService";
      cb.Register(c => A.Fake<ITestClass>()).Named<ITestClass>(serviceName);
      var container = cb.Build();

      IoC.Initialize(container);
      IoC.IsInitialized.Should().BeTrue();
      var testClass = IoC.TryResolve<ITestClass>(serviceName, defaultTestClass);
      testClass.Should().NotBeNull();
      testClass.Should().NotBeSameAs(defaultTestClass);
      var otherClass = IoC.TryResolve<IOtherClass>(serviceName, defaultOtherClass);
      otherClass.Should().NotBeNull();
      otherClass.Should().BeSameAs(defaultOtherClass);
    }
    #endregion
  }
}
