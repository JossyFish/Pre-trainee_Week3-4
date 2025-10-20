using WK_34.Entities;

namespace WK_34.Data
{
    public static class LibraryInitializer
    {
        public static void Initialize(LibraryContext context, IConfiguration configuration)
        {

            if (!context.Database.CanConnect())
            {
                context.Database.EnsureCreated();
            }

            if (!context.Authors.Any())
            {
                var seedData = configuration.GetSection("AuthorsBooksOptions").Get<AuthorsBooksOptions>();

                if (seedData?.Authors != null)
                {
                    var authors = seedData.Authors.Select((a) => new AuthorEntity
                    {
                        Name = a.Name,
                        DateOfBirth = a.DateOfBirth
                    }).ToList();

                    context.Authors.AddRange(authors);
                    context.SaveChanges();

                    if (seedData.Books != null)
                    {
                        var savedAuthors = context.Authors.ToList();

                        var books = seedData.Books.Select((b) => new BookEntity
                        {
                            Title = b.Title,
                            PublishedYear = b.PublishedYear,
                            AuthorId = savedAuthors[b.AuthorId - 1].Id
                        }).ToList();

                        context.Books.AddRange(books);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
