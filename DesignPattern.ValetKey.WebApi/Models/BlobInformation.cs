using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DesignPattern.ValetKey.WebApi.Models
{
    public class BlobInformation
    {
        [JsonProperty("container", Required = Required.Always)]
        public string Container { get; }

        [JsonProperty("blobName", Required = Required.Always)]
        public string BlobName { get; }
    }
}
