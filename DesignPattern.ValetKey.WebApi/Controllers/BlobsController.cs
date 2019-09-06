﻿using DesignPattern.ValetKey.Blob.Interfaces;
using DesignPattern.ValetKey.Blob.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DesignPattern.ValetKey.WebApi.Controllers
{
    [Route("blob-sas-management")]
    [ApiController]
    public class BlobsController : ControllerBase
    {
        private readonly IBlobSasManager _blobManager;
        private readonly ILogger<BlobsController> _logger;

        public BlobsController(
            IBlobSasManager blobManager,
            ILogger<BlobsController> logger)
        {
            _blobManager = blobManager;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BlobSignatureResponse> Create([FromBody] BlobSignatureRequest request)
        {
            try
            {
                return Ok(_blobManager.GenerateStorageAccessSignature(request));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while creating blob signature : {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> Update([FromBody] BlobSignatureRequest request)
        {
            string result;
            try
            {
                result = _blobManager.UpdateStorageAccessSignature(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while creating blob signature : {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return Ok(result);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> Delete([FromBody] BlobSignatureRequest request)
        {
            try
            {
                _blobManager.DeleteStorageAccessSignature(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while creating blob signature : {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return Ok();
        }
    }
}
