namespace TaskManager.SharedLayer.RequestModels.Projects
{
    public class AddProjectMembersDto
    {
        public List<int> MemberIds { get; set; } = [];

        public int MemberRole { get; set; }
    }
}
