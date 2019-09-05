using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Storage.Blob;

namespace DesignPattern.ValetKey.Blob.Interfaces
{
    public interface IBlobConnection
    {
        CloudBlobClient GetCloudBlobClient();
    }
}
