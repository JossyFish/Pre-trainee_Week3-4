namespace WK_34.Models
{
    public class Book
    {
        private Book(int id, string title, DateTime publishedYear, int authorId)
        {
            Id = id;
            Title = title;
            PublishedYear = publishedYear;
            AuthorId = authorId;
        }

        private Book(string title, DateTime publishedYear, int authorId)
        {
            Title = title;
            PublishedYear = publishedYear;
            AuthorId = authorId;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishedYear { get; set; }
        public int AuthorId { get; set; }

        public static Book Create(int id, string title, DateTime publishedYear, int authorId)
        {
            return new Book(id, title, publishedYear, authorId);
        }

        public static Book Create(string title, DateTime publishedYear, int authorId)
        {
            return new Book(title, publishedYear, authorId);
        }
    }
}
