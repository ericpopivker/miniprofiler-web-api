# MiniProfiler for Web Api
Display Mini-Profiler results returned from Web Api.

MiniProfiler comes with a way to return MiniProfiler data from WCF service, but not Web Api.  I provided a small library that allows you to return MiniProfiler performance data from Web Api to display it on the client site.  It works similar to WCF.

https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet#links

## Setup

1. Setup MiniProfiler on your Web Api using same steps as you would do it for a regular MVC project.

  List of steps is here:
   http://stackoverflow.com/a/31568406/221291

   Follow EF steps if you would like to track Entity Framework peformance.

   Or on original MiniProfiler site:
   http://miniprofiler.com

2. Download and add "MiniProfilerX.Profiling.WebApi" dll as reference to your WebApi project
3. Add the following code to Global.asax.cs in WebApi project

    ```C#
   using MiniProfilerX.Profiling.WebApi;  //Add this using pragma

   protected void Application_EndRequest()
   {
      MiniProfiler.Stop();

      //Add these lines
      if (MiniProfiler.Current != null)
       qMiniProfiler.Current.AddToHttpResponseHeader(Response);
   }
  ```





## Dependencies

* .NET Framework 4.6.1
* MiniProfiler 3.2.0.157
