using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.ValetKey.File.Interfaces
{
    public interface IFileSas
    {
        string GenerateSasUriWithReadPermission(string fileShare, string directory, string file);
        string GenerateSasUriWithWritePermission(string fileShare, string directory, string file);
        string GenerateSasUriWithDeletePermission(string fileShare, string directory, string file);
        string GenerateSasUriWithCreatePermission(string fileShare, string directory, string file);
    }
}
