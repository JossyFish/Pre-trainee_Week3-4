namespace WK_34.Entities
{
    public class BookEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishedYear { get; set; }
        public int AuthorId { get; set; }


        public AuthorEntity Author { get; set; }
    }
}
