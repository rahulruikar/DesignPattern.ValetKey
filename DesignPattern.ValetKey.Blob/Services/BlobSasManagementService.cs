using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Sas;
using DesignPattern.ValetKey.Blob.Interfaces;
using DesignPattern.ValetKey.Blob.Models;
using Microsoft.Extensions.Configuration;
using static DesignPattern.ValetKey.Blob.Models.Permissions;

namespace DesignPattern.ValetKey.Blob.Services
{
    public sealed class BlobSasManagementService : IBlobSasManager
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

        public BlobSignatureResponse CreateStorageAccessSignature(BlobSignatureRequest request)
        {
            _logger.LogInformation($"Creating SAS for container : {request.ContainerName} and blob : {request.BlobName}");

            var sharedKey = GetSharedKey();
            var client = GetContainerClient(request.ContainerName);
            var policyName = Guid.NewGuid().ToString();
            var accessPolicy = CreateAccessPolicy(policyName, ConvertToBlobSasPermissions(request.Permission));

            ContainerAccessPolicy existingContainerAccessPolicy = client.GetAccessPolicy();
            IList<SignedIdentifier> existingSignedIdentifiers = existingContainerAccessPolicy.SignedIdentifiers.ToList();
            existingSignedIdentifiers.Add(accessPolicy);
            ContainerInfo info = client.SetAccessPolicy(null, existingSignedIdentifiers);
            var blobSasBuilder = ConstructBlobSas(request.ContainerName, request.BlobName, policyName, ConvertToBlobSasPermissions(request.Permission));

            return new BlobSignatureResponse()
            {
                ContainerName = request.ContainerName,
                BlobName = request.BlobName,
                SharedAccessSignature = blobSasBuilder.ToSasQueryParameters(sharedKey).ToString(),
                PolicyName = policyName
            };
        }

        public BlobSignatureResponse UpdateStorageAccessSignature(BlobSignatureRequest request)
        {
            _logger.LogInformation($"Updating SAS for container : {request.ContainerName} and Blob : {request.BlobName}");

            var client = GetContainerClient(request.ContainerName);
            ContainerAccessPolicy containerAccessPolicy = client.GetAccessPolicy();
            IList<SignedIdentifier> existingSignedIdentifiers = containerAccessPolicy.SignedIdentifiers.ToList();
            Parallel.ForEach(existingSignedIdentifiers.Where(x => x.Id == request.PolicyName),
                z => z.AccessPolicy.Expiry = z.AccessPolicy.Expiry.AddMinutes(15));
            client.SetAccessPolicy(null, existingSignedIdentifiers);

            return new BlobSignatureResponse()
            {
                ContainerName = request.ContainerName,
                BlobName = request.BlobName,
                PolicyName = request.PolicyName
            };
        }

        
        public string DeleteStorageAccessSignature(BlobSignatureRequest request)
        {
            _logger.LogInformation($"Deleting SAS for container : {request.ContainerName} and Blob : {request.BlobName}");

            var client = GetContainerClient(request.ContainerName);
            ContainerAccessPolicy containerAccessPolicy = client.GetAccessPolicy();
            IList<SignedIdentifier> signedIdentifiers = containerAccessPolicy.SignedIdentifiers.Where(x => x.Id != request.PolicyName).ToList();
            client.SetAccessPolicy(null, signedIdentifiers, null);

            return "Shared Access Signature Removed";
        }

        private BlobContainerClient GetContainerClient(string containerName)
        {
            var sharedKey = GetSharedKey();
            var blobServiceUri = new Uri($"https://{_configuration.GetSection("StorageAccount")["AccountName"]}.blob.core.windows.net/{containerName}");
            return new BlobContainerClient(blobServiceUri, sharedKey);
        }

        private StorageSharedKeyCredential GetSharedKey()
        {
            return new StorageSharedKeyCredential(_configuration.GetSection("StorageAccount")["AccountName"],_configuration.GetSection("StorageAccount")["SharedKey"]);
        }

        private BlobSasBuilder ConstructBlobSas(string containerName, string blobName, string policyName, BlobSasPermissions permissions)
        {
            BlobSasBuilder blobSasBuilder = new BlobSasBuilder()
            {
                ContainerName = containerName,
                BlobName = blobName,
                Resource = "b",
                StartTime = DateTime.Now.AddMinutes(-15),
                Permissions = permissions.ToString(),
                Identifier = policyName
            };
            return blobSasBuilder;
        }

        private SignedIdentifier CreateAccessPolicy(string policyName, BlobSasPermissions permission)
        {
            return new SignedIdentifier()
            {
                AccessPolicy = new AccessPolicy()
                {
                    Expiry = DateTime.Now.AddMinutes(30),
                    Permission = permission.ToString(),
                    Start = DateTime.Now.AddMinutes(-15)
                },
                Id = policyName
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

