using System;
using System.Collections.Generic;
using System.Text;
using DesignPattern.ValetKey.Blob.Interfaces;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DesignPattern.ValetKey.Blob.Services
{
    public class BlobConnection : IBlobConnection
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BlobConnection> _logger;

        public BlobConnection(
            IConfiguration configuration,
            ILogger<BlobConnection> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public CloudBlobClient GetCloudBlobClient()
        {
            try
            {
                var cloudStorageAccount =
                    CloudStorageAccount.Parse(_configuration.GetSection("StorageAccount")["ConnectionString"]);
                return cloudStorageAccount.CreateCloudBlobClient();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while creating blob client : {ex.Message}");
                throw ex;
            }
        }
    }
}
