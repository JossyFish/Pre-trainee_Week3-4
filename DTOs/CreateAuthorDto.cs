using System.ComponentModel.DataAnnotations;

namespace WK_34.DTOs
{
    public class CreateAuthorDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}
