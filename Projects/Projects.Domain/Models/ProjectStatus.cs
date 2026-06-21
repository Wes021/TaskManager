using System.ComponentModel.DataAnnotations;

namespace Projects.Projects.Domain.Models
{
    public class ProjectStatus
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
