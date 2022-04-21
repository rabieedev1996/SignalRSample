using Microsoft.AspNetCore.Mvc;
using SignalRSample.Classes;

namespace SignalRSample.Controllers
{
    public class HomeController : Controller
    {
        public SampleClass _sampleClass;
        public HomeController(SampleClass sampleClass)
        {
            _sampleClass = sampleClass;
        }
        public IActionResult SendTest()
        {
            _sampleClass.SendBroadcastMessageSample();
            return Ok("Ok");
        }
    }
}
