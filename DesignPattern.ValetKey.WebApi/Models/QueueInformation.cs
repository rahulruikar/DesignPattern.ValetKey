using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DesignPattern.ValetKey.WebApi.Models
{
    public class QueueInformation
    {
        [JsonProperty("queueName", Required = Required.Always)]
        public string QueueName { get;}
    }
}
