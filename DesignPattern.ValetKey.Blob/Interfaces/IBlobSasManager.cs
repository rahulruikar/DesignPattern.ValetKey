using DesignPattern.ValetKey.Blob.Models;

namespace DesignPattern.ValetKey.Blob.Interfaces
{
    public interface IBlobSasManager
    {
        BlobSignatureResponse CreateStorageAccessSignature(BlobSignatureRequest request);
        BlobSignatureResponse UpdateStorageAccessSignature(BlobSignatureRequest request);
        string DeleteStorageAccessSignature(BlobSignatureRequest request);
    }
}
