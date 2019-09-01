using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.ValetKey.Queue.Interfaces
{
    public interface IQueueSas
    {
        string GenerateSasUriWithReadPermission(string queue);
        string GenerateSasUriWithAddPermission(string queue);
        string GenerateSasUriWithUpdatePermission(string queue);
    }
}
