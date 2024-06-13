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
using Microsoft.Extensions.Logging;

namespace AppFileUploader.Application.Features.FileContents.Commands
{
    public class AddContentCommandWrap 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
    }
    public class AddContentCommand : IRequest<FileContent>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
        public DateTime UploadTime { get; set; }
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

        public AddContentCommandHandler(IFileContent<FileContent> repository,  IMapper mapper, ILogger<AddContentCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<FileContent> Handle(AddContentCommand request, CancellationToken cancellationToken)
        {
            var CmdEnt = _mapper.Map<FileContent>(request);
            var newObj = await _repository.AddAsync(CmdEnt);
            return newObj;
        }
    }

}