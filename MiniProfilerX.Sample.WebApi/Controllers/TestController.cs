using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using StackExchange.Profiling;

namespace MiniProfilerX.Sample.WebApi.Controllers
{
    public class TestController : ApiController
    {
        const string Query = "SELECT * FROM Customer WHERE IsDeleted=1";
        const string Query2 = "SELECT * FROM Order WHERE CustomerId=2";

        /// <summary>
        /// Method that demonstates getting MiniProfiler performance data to be displayed in client application
        /// </summary>
        /// <returns></returns>
        /// 
        /// 
        [Route("~/test")]
        public async Task<string> Get()
        {
            using (MiniProfiler.StepStatic("WebApi: 1.0 Short Action"))
            {
                using (MiniProfiler.Current.CustomTiming("sql", Query))
                {
                    await Task.Delay(50);
                }

                using (MiniProfiler.Current.CustomTiming("redis", "Redis_Get (Key: Key1)"))
                {
                    await Task.Delay(20);
                }

                using (MiniProfiler.StepStatic("WebApi: 1.1 Inner Action"))
                {
                    await Task.Delay(20);
                }

                using (MiniProfiler.StepStatic("WebApi: 1.2 Another Inner Action"))
                {
                    await Task.Delay(20);
                }
            }


            using (MiniProfiler.StepStatic("WebApi: 2.0 Long Action"))
            {
                using (MiniProfiler.Current.CustomTiming("sql", Query2))
                {
                    await Task.Delay(500);
                }

                using (MiniProfiler.Current.CustomTiming("redis", "Redis_Set (Key: Key2)"))
                {
                    await Task.Delay(200);
                }

                using (MiniProfiler.StepStatic("WebApi: 2.1 InnerAction"))
                {
                    await Task.Delay(100);
                }

                using (MiniProfiler.StepStatic("WebApi: 2.2 Another InnerAction"))
                {
                    await Task.Delay(100);
                }

            }
        
            return "Check out all the MiniProfiler data from this API call";
        }
    }
}
