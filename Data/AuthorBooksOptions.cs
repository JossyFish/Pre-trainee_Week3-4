using WK_34.Models;

namespace WK_34.Data
{
    public class AuthorsBooksOptions
    {
        public AuthorOptions[] Authors { get; set; } = [];
        public BookOptions[] Books { get; set; } = [];
    }


    public class AuthorOptions
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class BookOptions
    {
        public string Title { get; set; }
        public DateTime PublishedYear { get; set; }
        public int AuthorId { get; set; }
    }

}
