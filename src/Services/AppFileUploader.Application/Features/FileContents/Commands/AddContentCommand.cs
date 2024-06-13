using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppFileUploader.Application.Contract.Persistence;
using AppFileUploader.Domain.Entities;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AppFileUploader.Application.Features.FileContents.Commands
{
    public class AddContentCommandIntWrap 
    {       
        public string Name { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public DateTime UploadTime { get; set; }
    }
    public class AddContentCommand : IRequest<FileContent>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
    }

    internal class AddContentCommandValidator : AbstractValidator<AddContentCommand>
    {
        public AddContentCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("File Name is required.")
                .NotNull();
        }
    }

    internal class AddContentCommandHandler : IRequestHandler<AddContentCommand, FileContent>
    {
        private readonly IFileContent<FileContent> _repository;
        
        private readonly IMapper _mapper;
        private readonly ILogger<AddContentCommandHandler> _logger;
        private readonly IConfiguration _configuration;

        public AddContentCommandHandler(IFileContent<FileContent> repository,  IMapper mapper, ILogger<AddContentCommandHandler> logger, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<FileContent> Handle(AddContentCommand request, CancellationToken cancellationToken)
        {
            if (request.File.Length > 0)
            {
                try
                {
                    AddContentCommandIntWrap request2 = new AddContentCommandIntWrap()
                    {
                        Description = request.Description,
                        Name = request.Name,
                        Filename = request.File.FileName,
                        UploadTime = DateTime.UtcNow
                    };
                    using (FileStream fs = File.Create(_configuration.GetSection("UploadPath").Value + request.File.FileName))
                    {
                        request.File.CopyTo(fs);
                        fs.Flush();                        
                        var CmdEnt = _mapper.Map<FileContent>(request2);
                        var newObj = await _repository.AddAsync(CmdEnt);
                        return newObj;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            return null;            
        }
    }

}