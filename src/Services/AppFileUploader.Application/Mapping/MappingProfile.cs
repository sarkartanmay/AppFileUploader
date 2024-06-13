using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppFileUploader.Application.Features.FileContents.Commands;
using AppFileUploader.Domain.Entities;
using AutoMapper;

namespace AppFileUploader.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FileContent, AddContentCommandIntWrap>().ReverseMap();   
        }        
    }
}