using Coldairarrow.DotNettyRPC;
using Common;
using System.Diagnostics;
using System.Web.Mvc;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            IHello client = RPCClientFactory.GetClient<IHello>("127.0.0.1", 9999);
            client.SayHello("aa");
            stopwatch.Stop();
            return Content(stopwatch.ElapsedMilliseconds.ToString());
        }
    }
}