using AutoMapper;
using Projects.Projects.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.SharedLayer.ResponseModels.Projects;

namespace Projects.Projects.Application.MappingProfiles
{
    public class ProjectsProfile : Profile
    {
        public  ProjectsProfile()
        {
            CreateMap<Project, ProjectInfoDto>()
             .ForMember(o => o.Status, opt => opt.MapFrom(src => src.Status.Name));





        }
    }
}
