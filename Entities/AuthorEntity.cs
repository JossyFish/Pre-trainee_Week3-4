namespace WK_34.Entities
{
    public class AuthorEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }


        public ICollection<BookEntity> Books { get; set; } = [];
    }
}
