using System.ComponentModel.DataAnnotations;

namespace WK_34.DTOs
{
    public class CreateBookDto
    {
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        public DateTime PublishedYear { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}
