Hazware.Core
============

Everyone seems to have their own collection of utility code they use over and over again. I am 
no exception. However, I am trying to refactor my single library of a few hundred classes, many 
of which implement functionality now available in .NET 4, or other more well developed libraries. 
My goal is to implement the minimum amount of code I rely on, that cannot be easily replaced by 
another available library. Why? I have a general philosophy for code that I call the Race to Zero. 
It is about reducing the amount of code I need to support to achieve my goal. With less code, 
comes less support needs, and that allows me to focus on the key functionality.

Source code to the [Hazware.Core nuget package](http://nuget.org/List/Packages/Hazware.Core).
See [github](https://github.com/dbuksbaum/Hazware.Core)

__Version 2.0 - 2011/09/XX__
  * Added usage of FluentAssertions to the UnitTests
  * Added ResolveAnythingSource
  * **Added dependency on Autofac 2.5.2.830**
  * Added some documentation to the wiki (https://github.com/dbuksbaum/Hazware.Core/wiki)
  + Added container support (Autofac based)
  + Added Action extension methods
  + Added Func extension methods
  + Added IEnumerable extension methods
  + Added IQueryable extension methods
  + Added String extension methods
  + Added IProvider interface
  + Added INamedProvider interface
  + Added unit tests for container support
  + Added default logging support
  + Added container support for logging
  _TODO_
  + Add unit tests for IEnumerable extension methods (_in process_)
  + Add unit tests for Action extension methods
  + Add unit tests for Func extension methods
  + Add unit tests for IQueryable extension methods
  + Add unit tests for String extension methods
  + Add unit test for DisposableAction
  + Add unit test for RepeatableDisposableAction

__Version 1.0 - 2011/08/14__

  * Initial Check In
  * Implemented IDisposal2, AbstractDisposal, DisposableAction, RepeatableDisposableAction, and CatalogConfigurator
  * Compiled for .NET 4, NET 4 Client Profile, and Silverlight 4
  * Create NuGet package for Hazware.Core