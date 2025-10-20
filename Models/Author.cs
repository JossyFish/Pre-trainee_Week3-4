namespace WK_34.Models
{
    public class Author
    {
        private Author (int id, string name, DateTime dateOfBirth)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        private Author(string name, DateTime dateOfBirth)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
        }


        public int Id {  get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }


        public static Author Create (int id, string name, DateTime dateOfBirth)
        {
            return  new Author(id, name, dateOfBirth);   
        }

        public static Author Create(string name, DateTime dateOfBirth)
        {
            return new Author(name, dateOfBirth);
        }


    }
}
