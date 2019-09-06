using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DesignPattern.ValetKey.Blob.Models
{
    public class BlobSignatureResponse
    {
        [JsonProperty("containerName")]
        public string ContainerName { get; set; }

        [JsonProperty("blobName")]
        public string BlobName { get; set; }

        [JsonProperty("sharedAccessSignature")]
        public string SharedAccessSignature { get; set; }
    }
}
