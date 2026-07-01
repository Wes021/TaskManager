using AutoMapper;
using TaskManager.SharedLayer.RequestModels.Identity;
using TaskManager.SharedLayer.RequestModels.Tasks.TaskHistory;
using TaskManager.SharedLayer.ResponseModels.Tasks;
using Tasks.Tasks.Domain.Models;

namespace Tasks.Tasks.Application.MappingProfiles
{
    public class TasksProfile : Profile
    {
        public TasksProfile()
        {
            CreateMap<Tasks.Domain.Models.Tasks, TaskInfoDetails>()
                 .ForMember(
        dest => dest.TasksStatusName,
        opt => opt.MapFrom(src => src.TasksStatus.Name))


                 ;

            CreateMap<TaskAttachments, TaskAttachmentsDto>();

            CreateMap<UserLookupDto, TaskMembersDto>();

            CreateMap<TaskComments, TaskCommentsDto>();

            CreateMap<TaskHistory, TaskHistoryDTO>();

        }

    }
}
