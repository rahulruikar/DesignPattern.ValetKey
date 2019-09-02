using DesignPattern.ValetKey.Queue.Interfaces;
using System;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DesignPattern.ValetKey.Queue.Services
{
    public class QueueSasGeneratorService : IQueueSas
    {
        private readonly CloudQueueClient _cloudQueueClient;
        private readonly ILogger<QueueSasGeneratorService> _logger;

        public QueueSasGeneratorService(
            ILogger<QueueSasGeneratorService> logger,
            IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            var cloudStorageAccount = CloudStorageAccount.Parse(configuration.GetSection("Secret")["SecretName"]);
            _cloudQueueClient = cloudStorageAccount.CreateCloudQueueClient();
        }

        public string GenerateSasUriWithReadPermission(string queue)
        {
            return GetSasUriFor(queue, SharedAccessQueuePermissions.Read);
        }

        public string GenerateSasUriWithAddPermission(string queue)
        {
            return GetSasUriFor(queue, SharedAccessQueuePermissions.Add);
        }

        public string GenerateSasUriWithUpdatePermission(string queue)
        {
            return GetSasUriFor(queue, SharedAccessQueuePermissions.Update);
        }

        private string GetSasUriFor(string queue, SharedAccessQueuePermissions permission)
        {
            try
            {
                var queueReference = _cloudQueueClient.GetQueueReference(queue);
                var sharedAccessPolicy = new SharedAccessQueuePolicy()
                {
                    SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
                    Permissions = permission
                };
                var sasFileToken = queueReference.GetSharedAccessSignature(sharedAccessPolicy);
                return queueReference.Uri + sasFileToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(($"Error Message : {ex.Message}");
                return String.Empty;
            }
        }
    }
}
