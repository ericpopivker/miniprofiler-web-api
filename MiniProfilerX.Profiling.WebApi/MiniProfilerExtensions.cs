using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using StackExchange.Profiling;
using System.Net.Http;


namespace MiniProfilerX.Profiling.WebApi
{
    public static class MiniProfilerExtensions
    {
        public const string MiniProfilerResultsHeaderName = "x-miniprofiler-results";

        public static void AddToHttpResponseHeaders(this MiniProfiler miniProfiler, HttpResponse httpResponse)
        {
            string miniProfilerJson = JsonConvert.SerializeObject(miniProfiler);
            string miniProfilerJsonCompressed = GZipUtils.Zip(miniProfilerJson);

            httpResponse.Headers.Add(MiniProfilerResultsHeaderName, miniProfilerJsonCompressed);
        }

        public static void TryAddMiniProfilerResultsFromHeader(this MiniProfiler miniProfiler, HttpResponseMessage httpResponseMessage)
        {
            IEnumerable<string> values;
            if (httpResponseMessage.Headers.TryGetValues(MiniProfilerResultsHeaderName, out values))
            {
                string miniProfilerJsonCompressed = values.First();
                string miniProfilerJson = GZipUtils.Unzip(miniProfilerJsonCompressed);
                MiniProfiler remoteMiniProfiler = JsonConvert.DeserializeObject<MiniProfiler>(miniProfilerJson);

                miniProfiler.AddProfilerResults(remoteMiniProfiler);
            }
        }
    }
}
