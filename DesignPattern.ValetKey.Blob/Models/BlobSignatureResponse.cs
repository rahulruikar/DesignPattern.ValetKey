using Newtonsoft.Json;

namespace DesignPattern.ValetKey.Blob.Models
{
    public sealed class BlobSignatureResponse
    {
        [JsonProperty("containerName")]
        public string ContainerName { get; set; }

        [JsonProperty("blobName")]
        public string BlobName { get; set; }

        [JsonProperty("sharedAccessSignature")]
        public string SharedAccessSignature { get; set; }

        [JsonProperty("policyName")]
        public string PolicyName { get; set; }
    }
}
