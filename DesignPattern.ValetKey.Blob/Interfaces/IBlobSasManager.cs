using DesignPattern.ValetKey.Blob.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesignPattern.ValetKey.Blob.Interfaces
{
    public interface IBlobSasManager
    {
        BlobSignatureResponse GenerateStorageAccessSignature(BlobSignatureRequest request);
        string UpdateStorageAccessSignature(BlobSignatureRequest request);
        string DeleteStorageAccessSignature(BlobSignatureRequest request);
    }
}
