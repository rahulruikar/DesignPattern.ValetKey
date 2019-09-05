using DesignPattern.ValetKey.Blob.Interfaces;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Logging;
using System;

namespace DesignPattern.ValetKey.Blob.Services
{
    public class BlobSasGeneratorService : IBlobSas
    {
        private readonly ILogger<BlobSasGeneratorService> _logger;
        private readonly CloudBlobClient _client;

        public BlobSasGeneratorService(
            ILogger<BlobSasGeneratorService> logger,
            IBlobConnection connection)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _client = connection?.GetCloudBlobClient() ?? throw new ArgumentNullException((nameof(connection)));
        }

        public string GenerateSasUriWithCreatePermission(string container, string blob)
        {
            return GetSasUriFor(container, blob, SharedAccessBlobPermissions.Create);
        }

        public string GenerateSasUriWithDeletePermission(string container, string blob)
        {
            return GetSasUriFor(container, blob, SharedAccessBlobPermissions.Delete);

        }

        public string GenerateSasUriWithReadPermission(string container, string blob)
        {
            return GetSasUriFor(container, blob, SharedAccessBlobPermissions.Read);
        }

        public string GenerateSasUriWithWritePermission(string container, string blob)
        {
            return GetSasUriFor(container, blob, SharedAccessBlobPermissions.Write);
        }

        private string GetSasUriFor(string containerName, string blobName, SharedAccessBlobPermissions permission)
        {
            try
            {
                var container = _client.GetContainerReference(containerName);
                var blob = container.GetBlockBlobReference(blobName);
                var sharedAccessPolicy = new SharedAccessBlobPolicy()
                {
                    SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
                    Permissions = permission
                };
                var sasBlobToken = blob.GetSharedAccessSignature(sharedAccessPolicy);
                return blob.Uri + sasBlobToken;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Message : {ex.Message}");
                return String.Empty;
            }
        }
    }
}
