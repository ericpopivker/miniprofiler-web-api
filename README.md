# MiniProfiler for Web Api
Display Mini-Profiler results returned from Web Api.

MiniProfiler comes with a way to return MiniProfiler data from WCF service, but not Web Api.  I provided a small library that allows you to return MiniProfiler performance data from Web Api to display it on the client site.  It works similar to WCF.

https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet#links

## Setup

### Web Api project
1. Setup MiniProfiler on your Web Api using same steps as you would do it for a regular MVC project.

  List of steps is here:
   http://stackoverflow.com/a/31568406/221291

   Follow EF steps if you would like to track Entity Framework peformance.

   Or on original MiniProfiler site:
   http://miniprofiler.com

2. Download and add "MiniProfilerX.Profiling.WebApi" dll and reference it in your WebApi project
   From here: https://github.com/ericpopivker/miniprofiler-web-api/releases
3. Add the following code to Global.asax.cs in WebApi project

    ```C#
   using MiniProfilerX.Profiling.WebApi;  //Add this using pragma
    ...
    
   protected void Application_EndRequest()
   {
      MiniProfiler.Stop();

      //Add these lines
      if (MiniProfiler.Current != null)
       qMiniProfiler.Current.AddToHttpResponseHeader(Response);
   }
  ```



### Client MVC project

1. Make sure  MiniProfiler is setup on your MVC project using steps here.  Similar to Web Api

  List of steps is here:
   http://stackoverflow.com/a/31568406/221291

   Follow EF steps if you would like to track Entity Framework peformance.

   Or on original MiniProfiler site:
   http://miniprofiler.com
   
2. Download and add "MiniProfilerX.Profiling.WebApi" dll and reference it in your WebApi project
   From here: https://github.com/ericpopivker/miniprofiler-web-api/releases
3. Add the following code to the code that creates and send Http requests to external APIs

   ```C#
   using MiniProfilerX.Profiling.WebApi;  //Add this using pragma
    ...
    
    public async Task<ActionResult> SomeControllerAction()
    {
      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri("http://localhost:5462/");
        ...
        HttpResponseMessage response = await client.SendAsync(request);
      
        MiniProfiler.Current.TryAddMiniProfilerResultsFromHeader(response);   //Add this line after recieving response from HttpClient
        ...
      }
    }
   ```

## Dependencies

* .NET Framework 4.6.1
* MiniProfiler 3.2.0.157
