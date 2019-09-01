namespace DesignPattern.ValetKey.Blob.Interfaces
{
    public interface IBlobSas
    {
        string GenerateSasUriWithReadPermission(string container, string blob);
        string GenerateSasUriWithWritePermission(string container, string blob);
        string GenerateSasUriWithDeletePermission(string container, string blob);
        string GenerateSasUriWithCreatePermission(string container, string blob);
    }
}
