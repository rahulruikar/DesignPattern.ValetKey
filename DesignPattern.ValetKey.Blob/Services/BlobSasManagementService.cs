using Azure.Storage;
using Azure.Storage.Sas;
using DesignPattern.ValetKey.Blob.Interfaces;
using DesignPattern.ValetKey.Blob.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using static DesignPattern.ValetKey.Blob.Models.Permissions;

namespace DesignPattern.ValetKey.Blob.Services
{
    public class BlobSasManagementService : IBlobSasManager
    {
        private readonly ILogger<BlobSasManagementService> _logger;
        private readonly IConfiguration _configuration;

        public BlobSasManagementService(
            IConfiguration configuration,
            ILogger<BlobSasManagementService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public BlobSignatureResponse GenerateStorageAccessSignature(BlobSignatureRequest request)
        {
            return CreateSasToken(request.ContainerName, request.BlobName, ConvertToBlobSasPermissions(request.Permission));
        }

        public string UpdateStorageAccessSignature(BlobSignatureRequest request)
        {
            throw new NotImplementedException();
        }

        public string DeleteStorageAccessSignature(BlobSignatureRequest request)
        {
            throw new NotImplementedException();
        }

        private BlobSignatureResponse CreateSasToken(string containerName, string blobName, BlobSasPermissions permission)
        {
            BlobSasBuilder blobSasBuilder = new BlobSasBuilder()
            {
                ContainerName = containerName,
                BlobName = blobName,
                Permissions = permission.ToString(),
                Resource = "b",
                StartTime = DateTimeOffset.Now,
                ExpiryTime = DateTimeOffset.Now.AddHours(24)
            };

            var sharedKeyCredentials = new StorageSharedKeyCredential(_configuration.GetSection("StorageAccount")["AccountName"], _configuration.GetSection("StorageAccount")["SharedKey"]);

            return new BlobSignatureResponse()
            {
                ContainerName = containerName,
                BlobName = blobName,
                SharedAccessSignature = blobSasBuilder.ToSasQueryParameters(sharedKeyCredentials).ToString()
            };
            
        }
        private BlobSasPermissions ConvertToBlobSasPermissions(Permissions permission)
        {
            BlobSasPermissions blobSasPermission = new BlobSasPermissions();

            switch (permission)
            {
                case Read:
                    blobSasPermission.Read = true;
                    break;
                case Create:
                    blobSasPermission.Create = true;
                    break;
                case Delete:
                    blobSasPermission.Delete = true;
                    break;
                case Write:
                    blobSasPermission.Write = true;
                    break;
            }
            return blobSasPermission;
        }
    }
}
