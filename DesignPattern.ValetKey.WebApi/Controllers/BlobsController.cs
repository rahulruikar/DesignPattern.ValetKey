﻿using System.Collections.Generic;
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

        [HttpGet]
        public ActionResult<string> ReadBlob([FromBody] BlobInformation blobInformation)
        {
            string url = _blobSas.GenerateSasUriWithReadPermission(blobInformation?.Container, blobInformation?.BlobName);
            return url;
        }

        [HttpDelete]
        public ActionResult<string> DeleteBlob([FromBody] BlobInformation blobInformation)
        {
            string url = _blobSas.GenerateSasUriWithDeletePermission(blobInformation?.Container, blobInformation?.BlobName);
            return url;
        }

        [HttpPost]
        public ActionResult<string> CreateBlob([FromBody] BlobInformation blobInformation)
        {
            string url = _blobSas.GenerateSasUriWithCreatePermission(blobInformation?.Container, blobInformation?.BlobName);
            return url;
        }

        [HttpPut]
        public ActionResult<string> UpdateBlob([FromBody] BlobInformation blobInformation)
        {
            string url = _blobSas.GenerateSasUriWithWritePermission(blobInformation?.Container, blobInformation?.BlobName);
            return url;
        }
    }
}
