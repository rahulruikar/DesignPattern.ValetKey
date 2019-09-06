using DesignPattern.ValetKey.Blob.Converters;
using Newtonsoft.Json;

namespace DesignPattern.ValetKey.Blob.Models
{
    public enum Permissions
    {
        Read,
        Write,
        Create,
        Delete,
        Unknown
    };

    public class BlobSignatureRequest
    {
        [JsonProperty("containerName", Required = Required.Always)]
        public string ContainerName { get; set; }

        [JsonProperty("blobName", Required = Required.Always)]
        public string BlobName { get; set; }

        [JsonProperty("sharedAccessSignature", Required = Required.Default)]
        public string SharedAccessSignature { get; set; }

        [JsonProperty("permission", Required = Required.Default)]
        [JsonConverter(typeof(PermissionsJsonConverter))]
        public Permissions Permission { get; set; }
    }
}
