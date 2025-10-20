using Microsoft.AspNetCore.Mvc;
using WK_34.Data.Interfaces;
using WK_34.Models;
using WK_34.Repositories;
using WK_34.Repositories.Interfaces;
using WK_34.Services.Interfaces;

namespace WK_34.Services
{
    public class BooksService : IBooksService
    {
        IBooksRepository _booksRepository;
        IBookValidator _validator;

        public BooksService(IBooksRepository booksRepository, IBookValidator validator)
        {
            _booksRepository = booksRepository;
            _validator = validator;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _booksRepository.GetAllAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            _validator.ValidateId(id);
            return await _booksRepository.GetByIdAsync(id);
        }

        public async Task<List<Book>> GetAllInIntervalAsync(DateTime startDate, DateTime endDate)
        {
            _validator.ValidateInterval(startDate, endDate);
            return await _booksRepository.GetAllInIntervalAsync(startDate, endDate);
        }

        public async Task<Book> CreateAsync(string title, DateTime publishedYear, int authorId)
        {
            _validator.ValidateTitle(title);
            _validator.ValidatePublishedYear(publishedYear);
            _validator.ValidateId(authorId);

            return await _booksRepository.CreateAsync(title, publishedYear, authorId);
        }

        public async Task<Book> UpdateByIdAsync(int id, string title, DateTime publishedYear, int authorId)
        {
            _validator.ValidateId(id);
            _validator.ValidateTitle(title);
            _validator.ValidatePublishedYear(publishedYear);
            _validator.ValidateId(authorId);

            var book = Book.Create(id, title, publishedYear, authorId);

            return await _booksRepository.UpdateByIdAsync(book);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            _validator.ValidateId(id);
            return await _booksRepository.DeleteByIdAsync(id);
        }
    }
}
