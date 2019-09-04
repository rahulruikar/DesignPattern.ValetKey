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

        [HttpGet]
        public ActionResult<string> Read([FromBody] FileInformation fileInformation)
        {
            var url =
                _fileSas.GenerateSasUriWithReadPermission(fileInformation?.FileShare, fileInformation?.Directory, fileInformation?.File);
            return url;
        }

        [HttpDelete]
        public ActionResult<string> Delete([FromBody] FileInformation fileInformation)
        {
            var url =
                _fileSas.GenerateSasUriWithReadPermission(fileInformation?.FileShare, fileInformation?.Directory, fileInformation?.File);
            return url;
        }

        [HttpPost]
        public ActionResult<string> Create([FromBody] FileInformation fileInformation)
        {
            var url =
                _fileSas.GenerateSasUriWithReadPermission(fileInformation?.FileShare, fileInformation?.Directory, fileInformation?.File);
            return url;
        }

        [HttpPut]
        public ActionResult<string> Update([FromBody] FileInformation fileInformation)
        {
            var url =
                _fileSas.GenerateSasUriWithReadPermission(fileInformation?.FileShare, fileInformation?.Directory, fileInformation?.File);
            return url;
        }
    }
}