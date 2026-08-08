using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projects.Projects.Domain.Models;

namespace Projects.Projects.Infrastructure.Configurations
{
    public class ProjectMembersRoleConfiguration : IEntityTypeConfiguration<ProjectMemberRole>
    {
        public void Configure(EntityTypeBuilder<ProjectMemberRole> builder)
        {
            builder.HasData(
           new ProjectMemberRole
           {
               Id = 1,
               Name = "Leader",
               IsActive = true,
               IsDeleted = false
           },
           new ProjectMemberRole
           {
               Id = 2,
               Name = "Member",
               IsActive = true,
               IsDeleted = false
           });
        }
    }
}
