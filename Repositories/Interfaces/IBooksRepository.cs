using WK_34.Models;

namespace WK_34.Repositories.Interfaces
{
    public interface IBooksRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task<List<Book>> GetAllInIntervalAsync(DateTime startDate, DateTime endDate);
        Task<Book> CreateAsync(string title, DateTime publishedYear, int authorId);
        Task<Book> UpdateByIdAsync(Book book);
        Task<bool> DeleteByIdAsync(int id);
    }
}