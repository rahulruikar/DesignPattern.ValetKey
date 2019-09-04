using DesignPattern.ValetKey.Blob.Interfaces;
using DesignPattern.ValetKey.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesignPattern.ValetKey.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobsController : ControllerBase
    {
        private readonly IBlobSas _blobSas;

        public BlobsController(IBlobSas blobSas)
        {
            _blobSas = blobSas;
        }

        [HttpGet("read/container/{container}/blob/{blobName}")]
        public ActionResult<string> Read(string container, string blobName)
        {
            string url = _blobSas.GenerateSasUriWithReadPermission(container, blobName);
            return url;
        }

        [HttpDelete("delete/container/{container}/blob/{blobName}")]
        public ActionResult<string> Delete(string container, string blobName)
        {
            string url = _blobSas.GenerateSasUriWithDeletePermission(container, blobName);
            return url;
        }

        [HttpPost]
        public ActionResult<string> Create([FromBody] BlobInformation blobInformation)
        {
            string url = _blobSas.GenerateSasUriWithCreatePermission(blobInformation.Container, blobInformation.BlobName);
            return url;
        }

        [HttpPut]
        public ActionResult<string> Update([FromBody] BlobInformation blobInformation)
        {
            string url = _blobSas.GenerateSasUriWithWritePermission(blobInformation.Container, blobInformation.BlobName);
            return url;
        }
    }
}
