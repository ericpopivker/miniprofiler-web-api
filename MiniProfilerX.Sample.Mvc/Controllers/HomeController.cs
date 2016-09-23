using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MiniProfilerX.Profiling.WebApi;
using StackExchange.Profiling;

namespace MiniProfilerX.Sample.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        [Route("call-web-api")]
        public async Task<ActionResult> CallWebApi()
        {
            string result;

            using (MiniProfiler.StepStatic("Mvc: Calling External API"))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5462/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "test");

                    // New code:
                    HttpResponseMessage response = await client.SendAsync(request);

                    MiniProfiler.Current.TryAddMiniProfilerResultsFromHeader(response);

                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        result = "fail";
                    }
                }
            }


            return View("Index", (object)result);
        }
    }
}