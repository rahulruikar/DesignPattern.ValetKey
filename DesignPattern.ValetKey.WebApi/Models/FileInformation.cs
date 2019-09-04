using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DesignPattern.ValetKey.WebApi.Models
{
    public class FileInformation
    {
        [JsonProperty("fileShare", Required = Required.Always)]
        public string FileShare { get; }

        [JsonProperty("directory", Required = Required.Always)]
        public string Directory { get; }

        [JsonProperty("file", Required = Required.Always)]
        public string File { get; }

    }
}
