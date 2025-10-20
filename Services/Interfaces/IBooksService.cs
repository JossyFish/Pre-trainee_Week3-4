using WK_34.Models;

namespace WK_34.Services.Interfaces
{
    public interface IBooksService
    {
        Task<List<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task<List<Book>> GetAllInIntervalAsync(DateTime startDate, DateTime endDate);
        Task<Book> CreateAsync(string title, DateTime publishedYear, int authorId);
        Task<Book> UpdateByIdAsync(int id, string title, DateTime publishedYear, int authorId);
        Task<bool> DeleteByIdAsync(int id);
    }
}