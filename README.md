# MiniProfiler for Web Api

MiniProfiler comes with a way to return MiniProfiler data from WCF service, but not Web Api.  I provided a small library that allows you to return MiniProfiler performance data from Web Api to display it on the client MVC Web applicaiton.  It works similar to how WCF MiniProfiler integration used to work.


![Screenshot](/Screenshot.png "Screenshot")

## Setup

### Web Api project
1. Setup MiniProfiler on your Web Api using same steps as you would do it for a regular MVC project.

  List of steps is here:
   http://stackoverflow.com/a/31568406/221291

   Follow EF steps if you would like to track Entity Framework peformance.

   Or on original MiniProfiler site:
   http://miniprofiler.com

2. Download and add "MiniProfilerX.Profiling.WebApi" dll and reference it in your WebApi project.
   
   From here: https://github.com/ericpopivker/miniprofiler-web-api/releases
3. Add the following 3 lines  to Global.asax.cs in WebApi project

    ```C#
   using MiniProfilerX.Profiling.WebApi;  //Add this  pragma
    ...
    
   protected void Application_EndRequest()
   {
      MiniProfiler.Stop();

      //Add these two lines
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
   
2. Download and add "MiniProfilerX.Profiling.WebApi" dll and reference it in your WebApi project.
   
   From here: https://github.com/ericpopivker/miniprofiler-web-api/releases
3. Add the following two line to the code that creates and sends Http requests to external APIs

   ```C#
   using MiniProfilerX.Profiling.WebApi;  //Add this pragma
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
