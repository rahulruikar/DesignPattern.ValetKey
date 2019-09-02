using DesignPattern.ValetKey.Blob.Interfaces;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DesignPattern.ValetKey.Blob.Services
{
    public class BlobSasGeneratorService : IBlobSas
    {
        private readonly CloudBlobClient _cloudBlobClient;
        private readonly ILogger<BlobSasGeneratorService> _logger;

        public BlobSasGeneratorService(
            ILogger<BlobSasGeneratorService> logger,
            IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            var cloudStorageAccount = CloudStorageAccount.Parse(configuration.GetSection("Secret")["SecretName"]);
            _cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient(); 
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
                var container = _cloudBlobClient.GetContainerReference(containerName);
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
