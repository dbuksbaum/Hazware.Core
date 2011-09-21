using System.Collections.Generic;
using System.Linq;
using System;
using NUnit.Framework;
using Hazware.Extensions;

// ReSharper disable CheckNamespace
namespace Hazware.Tests
// ReSharper restore CheckNamespace
{
  [TestFixture, Category("Extension Methods")]
// ReSharper disable InconsistentNaming
  public class IEnumerableExtensionTests
// ReSharper restore InconsistentNaming
  {
    #region Fields
    private IList<int> _emptyList;
    private IList<int> _smallList;
    private IList<int> _bigList;
    #endregion

    #region Setup / Teardown
    [SetUp]
    public void Setup()
    {
      _emptyList = new List<int>();
      _smallList = new List<int>(Enumerable.Range(0, 10));
      _bigList = new List<int>(Enumerable.Range(0, 10000));
    }
    #endregion

    #region Null Source
    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ForEachWithNullEnumerableThrows()
    {
      IEnumerable<int> myNullEnumerable = null;
      var count = 0;
// ReSharper disable ConditionIsAlwaysTrueOrFalse
      myNullEnumerable.ForEach(i => count++);
// ReSharper restore ConditionIsAlwaysTrueOrFalse
    }
    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ForEachConditionalWithNullEnumerableThrows()
    {
      IEnumerable<int> myNullEnumerable = null;
      var count = 0;
      // ReSharper disable ConditionIsAlwaysTrueOrFalse
      myNullEnumerable.ForEach(i => { count++; }, j => true);
      // ReSharper restore ConditionIsAlwaysTrueOrFalse
    }
    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ForEachStepWithNullEnumerableThrows()
    {
      IEnumerable<int> myNullEnumerable = null;
      var count = 0;
      // ReSharper disable ConditionIsAlwaysTrueOrFalse
      foreach (var item in myNullEnumerable.ForEachStep(i => count++))
      // ReSharper restore ConditionIsAlwaysTrueOrFalse
      { //  noop
      }
    }
    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ForEachStepConditionalWithNullEnumerableThrows()
    {
      IEnumerable<int> myNullEnumerable = null;
      var count = 0;
      // ReSharper disable ConditionIsAlwaysTrueOrFalse
      foreach (var item in myNullEnumerable.ForEachStep(i => count++, j => true))
      // ReSharper restore ConditionIsAlwaysTrueOrFalse
      { //  noop
      }
    }
    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ForEachWithNullActionThrows()
    {
      var count = 0;
      _emptyList.ForEach(null);
    }
    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ForEachConditionalWithNullActionThrows()
    {
      var count = 0;
      _emptyList.ForEach(null, j => true);
    }
    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ForEachStepWithNullActionThrows()
    {
      foreach(var item in _emptyList.ForEachStep(null))
      { //  NOOP
      }
    }
    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ForEachStepConditionalWithNullActionThrows()
    {
      foreach (var item in _emptyList.ForEachStep(null, j => true))
      { //  NOOP
      }
    }
    [Test]
    public void ForEachConditionalWithNullConditionWorks()
    {
      var count = 0;
      _smallList.ForEach(i => count++, null);
      Assert.AreEqual(_smallList.Count, count);
    }
    [Test]
    public void ForEachStepConditionalWithNullConditionWorks()
    {
      var count = 0;
      var stepCount = 0;
// ReSharper disable LoopCanBeConvertedToQuery
      foreach (var item in _smallList.ForEachStep(i => count++, null))
// ReSharper restore LoopCanBeConvertedToQuery
      {
        stepCount++;
      }
      Assert.AreEqual(_smallList.Count, count);
      Assert.AreEqual(count, stepCount);
    }
    #endregion

    #region Basic ForEach Functionality
    [Test]
    public void ForEachAcrossEmptyList()
    {
      var count = 0;
      _emptyList.ForEach(i => count++);
      Assert.AreEqual(_emptyList.Count(), count);
    }
    [Test]
    public void ForEachConditionalAcrossEmptyList()
    {
      var count = 0;
      _emptyList.ForEach(i => count++, j => true);
      Assert.AreEqual(_emptyList.Count(), count);
    }
    [Test]
    public void ForEachAcrossSmallList()
    {
      var count = 0;
      _smallList.ForEach(i => count++);
      Assert.AreEqual(_smallList.Count(), count);
    }
    [Test]
    public void ForEachConditionalAcrossSmallList()
    {
      var count = 0;
      _smallList.ForEach(i => count++, j => true);
      Assert.AreEqual(_smallList.Count(), count);
    }
    [Test]
    public void ForEachAcrossBigList()
    {
      var count = 0;
      _bigList.ForEach(i => count++);
      Assert.AreEqual(_bigList.Count(), count);
    }
    [Test]
    public void ForEachConditionalAcrossBigList()
    {
      var count = 0;
      _bigList.ForEach(i => count++, j => true);
      Assert.AreEqual(_bigList.Count(), count);
    }
    #endregion

    #region ForEach With Condition
    [Test]
    public void ForEachConditionalTest()
    {
      var count = 0;
      _bigList.ForEach(i => count++, j => j%2 == 0);
      Assert.AreEqual(_bigList.Count()/2, count);
    }
    #endregion

  }
}