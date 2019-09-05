using System;
using System.Collections.Generic;
using System.Text;
using DesignPattern.ValetKey.Blob.Interfaces;
using Microsoft.Extensions.Logging;

namespace DesignPattern.ValetKey.Blob.Services
{
    public class BlobManager : IBlobManager
    {
        private readonly IBlobConnection _connection;
        private readonly ILogger<BlobManager> _logger;

        public BlobManager(
            IBlobConnection connection,
            ILogger<BlobManager> logger)
        {
            _connection = connection;
            _logger = logger;
        }
        public void DeleteBlob(string url)
        {
            
        }
    }
}
