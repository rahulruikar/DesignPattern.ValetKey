using DesignPattern.ValetKey.File.Interfaces;
using DesignPattern.ValetKey.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesignPattern.ValetKey.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileSas _fileSas;

        public FilesController(IFileSas fileSas)
        {
            _fileSas = fileSas;
        }

        [HttpGet("read/fileShare/{fileShare}/directory/{directory}/file/{file}")]
        public ActionResult<string> Read(string fileShare, string directory, string file)
        {
            var url = _fileSas.GenerateSasUriWithReadPermission(fileShare, directory, file);
            return url;
        }

        [HttpDelete("delete/fileShare/{fileShare}/directory/{directory}/file/{file}")]
        public ActionResult<string> Delete(string fileShare, string directory, string file)
        {
            var url = _fileSas.GenerateSasUriWithReadPermission(fileShare, directory, file);
            return url;
        }

        [HttpPost]
        public ActionResult<string> Create([FromBody] FileInformation fileInformation)
        {
            var url = _fileSas.GenerateSasUriWithReadPermission(fileInformation.FileShare, fileInformation.Directory, fileInformation.File);
            return url;
        }

        [HttpPut]
        public ActionResult<string> Update([FromBody] FileInformation fileInformation)
        {
            var url = _fileSas.GenerateSasUriWithReadPermission(fileInformation.FileShare, fileInformation.Directory, fileInformation.File);
            return url;
        }
    }
}