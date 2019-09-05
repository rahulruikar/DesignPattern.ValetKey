using DesignPattern.ValetKey.Queue.Interfaces;
using DesignPattern.ValetKey.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesignPattern.ValetKey.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueuesController : ControllerBase
    {
        private readonly IQueueSas _queueSas;

        public QueuesController(IQueueSas queueSas)
        {
            _queueSas = queueSas;
        }

        [HttpGet("{queue}")]
        public ActionResult<string> Read(string queue)
        {
            var url = _queueSas.GenerateSasUriWithReadPermission(queue);
            return url;
        }

        [HttpDelete("{queue}")]
        public ActionResult<string> Send(string queue)
        {
            var url = _queueSas.GenerateSasUriWithAddPermission(queue);
            return url;
        }

        [HttpPut("{queue}")]
        public ActionResult<string> Update(string queue)
        {
            var url = _queueSas.GenerateSasUriWithUpdatePermission(queue);
            return url;
        }
    }
}