// WK_34/Repositories/BooksRepository.cs
using Microsoft.EntityFrameworkCore;
using WK_34.Data;
using WK_34.Data.Interfaces;
using WK_34.Entities;
using WK_34.Models;
using WK_34.Repositories.Interfaces;

namespace WK_34.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly LibraryContext _context;

        public BooksRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books
                .Select(b => Book.Create(b.Id, b.Title, b.PublishedYear, b.AuthorId))
                .ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            ValidateId(id);

            return await _context.Books
                .AsNoTracking()
                .Where(b => b.Id == id)
                .Select(b => Book.Create(b.Id, b.Title, b.PublishedYear, b.AuthorId))
                .FirstOrDefaultAsync();
        }

        public async Task<List<Book>> GetAllInIntervalAsync(DateTime startDate, DateTime endDate)
        {
            ValidatePublishedYear(startDate);
            ValidatePublishedYear(endDate);
            ValidateInterval(startDate, endDate);

            return await _context.Books
                .AsNoTracking()
                .Where(b => b.PublishedYear >= startDate && b.PublishedYear <= endDate)
                .Select(b => Book.Create(b.Id, b.Title, b.PublishedYear, b.AuthorId))
                .ToListAsync();
        }

        public async Task<Book> CreateAsync(string title, DateTime publishedYear, int authorId)
        {
            ValidateTitle(title);
            ValidatePublishedYear(publishedYear);
            ValidateId(authorId);

            await CheckAuthorExistById(authorId);

            var book = new BookEntity
            {
                Title = title.Trim(),
                PublishedYear = publishedYear,
                AuthorId = authorId
            };

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return Book.Create(book.Id, book.Title, book.PublishedYear, book.AuthorId);
        }

        public async Task<Book> UpdateByIdAsync(Book book)
        {
            ValidateId(book.Id);
            ValidateTitle(book.Title);
            ValidatePublishedYear(book.PublishedYear);
            ValidateId(book.AuthorId);

            await CheckAuthorExistById(book.AuthorId);

            var bookEntity = await GetEntityByIdAsync(book.Id);

            bookEntity.Title = book.Title.Trim();
            bookEntity.PublishedYear = book.PublishedYear;
            bookEntity.AuthorId = book.AuthorId;

            await _context.SaveChangesAsync();
            return Book.Create(bookEntity.Id, bookEntity.Title, bookEntity.PublishedYear, bookEntity.AuthorId);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            ValidateId(id);

            var bookEntity = await GetEntityByIdAsync(id);

            _context.Books.Remove(bookEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task CheckAuthorExistById (int id)
        {
            var authorExists = await _context.Authors.AnyAsync(a => a.Id == id);
            if (!authorExists)
                throw new Exception($"Author not found");
        }
        private async Task<BookEntity> GetEntityByIdAsync(int id)
        {
            var bookEntity = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bookEntity == null)
            {
                throw new Exception($"Book not found");
            }
            return bookEntity;
        }

        private void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id cannot be less than or equal to 0.");
        }

        private void ValidateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title.Trim()))
                throw new ArgumentException("Title must contain at least 1 character.");
        }

        private void ValidatePublishedYear(DateTime publishedYear)
        {
            if (publishedYear > DateTime.Now)
                throw new ArgumentException("Published year must be in the past.");
        }

        private void ValidateInterval(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("Start date cannot be greater than end date.");
        }
    }
}