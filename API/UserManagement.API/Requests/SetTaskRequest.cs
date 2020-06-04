using System.ComponentModel.DataAnnotations;

namespace UserManagement.API.Requests
{
    class SetTaskRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsDone { get; set; }
    }
}
