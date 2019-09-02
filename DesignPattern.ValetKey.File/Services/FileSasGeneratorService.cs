using DesignPattern.ValetKey.File.Interfaces;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.File;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DesignPattern.ValetKey.File.Services
{
    public class FileSasGeneratorService : IFileSas
    {
        private readonly CloudFileClient _cloudFileClient;
        private readonly ILogger<FileSasGeneratorService> _logger;
        private readonly IConfiguration _configuration;

        public FileSasGeneratorService(
            ILogger<FileSasGeneratorService> logger,
            IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration;
            var cloudStorageAccount = CloudStorageAccount.Parse(configuration.GetSection("Section")["SecretName"]);
            _cloudFileClient = cloudStorageAccount.CreateCloudFileClient();
        }
        public string GenerateSasUriWithReadPermission(string fileShare, string directory, string file)
        {
            return GetSasUriFor(fileShare, directory, file, SharedAccessFilePermissions.Read);
        }

        public string GenerateSasUriWithWritePermission(string fileShare, string directory, string file)
        {
            return GetSasUriFor(fileShare, directory, file, SharedAccessFilePermissions.Write);
        }

        public string GenerateSasUriWithDeletePermission(string fileShare, string directory, string file)
        {
            return GetSasUriFor(fileShare, directory, file, SharedAccessFilePermissions.Delete);
        }

        public string GenerateSasUriWithCreatePermission(string fileShare, string directory, string file)
        {
            return GetSasUriFor(fileShare, directory, file, SharedAccessFilePermissions.Create);
        }

        private string GetSasUriFor(string fileShare, string directory, string file, SharedAccessFilePermissions permission)
        {
            try
            {
                var fileShareReference = _cloudFileClient.GetShareReference(fileShare);
                var rootDirectoryReference = fileShareReference.GetRootDirectoryReference();
                var directoryReference = rootDirectoryReference.GetDirectoryReference(directory);
                var fileReference = directoryReference.GetFileReference(file);

                var sharedAccessPolicy = new SharedAccessFilePolicy()
                {
                    SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
                    Permissions = permission
                };
                var sasFileToken = fileReference.GetSharedAccessSignature(sharedAccessPolicy);
                return fileReference.Uri + sasFileToken;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Message : {ex.Message}");
                return String.Empty;
            }
        }
    }
}
