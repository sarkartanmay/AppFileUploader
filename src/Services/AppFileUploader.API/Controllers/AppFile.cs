using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AppFileUploader.Application.Features.FileContents.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace AppFileUploader.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AppFile : ControllerBase
    {
        private readonly ILogger<AppFile> _logger;
        private readonly IMediator _mediator;

        public AppFile(IMediator mediator, ILogger<AppFile> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        //[Authorize(Policy = "P1")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> Save([FromForm] AddContentCommandWrap command)
        {
            if (command.File.Length > 0)
            {
                try
                {
                    AddContentCommand cmd = new AddContentCommand()
                    {
                        Description = command.Description,
                        Name = command.Name,
                        Filename = command.File.FileName,
                        UploadTime = DateTime.UtcNow
                    };

                    using (FileStream fs = System.IO.File.Create(".\\Uploads\\" + command.File.FileName))
                    {
                        command.File.CopyTo(fs);
                        fs.Flush();
                        var result = await _mediator.Send(cmd);
                        return Ok(result);
                    }
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            var result2 = HttpStatusCode.InternalServerError;
            return Ok(result2);
        }
        
    }
}