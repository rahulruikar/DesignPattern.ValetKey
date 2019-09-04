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

        [HttpGet]
        public ActionResult<string> Read([FromBody] QueueInformation queueInformation)
        {
            var url = _queueSas.GenerateSasUriWithReadPermission(queueInformation.QueueName);
            return url;
        }

        [HttpDelete]
        public ActionResult<string> Send([FromBody] QueueInformation queueInformation)
        {
            var url = _queueSas.GenerateSasUriWithAddPermission(queueInformation.QueueName);
            return url;
        }

        [HttpPut]
        public ActionResult<string> Update([FromBody] QueueInformation queueInformation)
        {
            var url = _queueSas.GenerateSasUriWithUpdatePermission(queueInformation.QueueName);
            return url;
        }
    }
}