using WK_34.DTOs;
using WK_34.Models;

namespace WK_34.Services.Interfaces
{
    public interface IAuthorsService
    {
        Task<List<Author>> GetAllAsync();
        Task<List<AuthorWithBooksCountDto>> GetAuthorsWithBooksAmountAsync();
        Task<Author> GetByNameAsync(string name);
        Task<Author> GetByIdAsync(int id);
        Task<Author> CreateAsync(string name, DateTime dateOfBirth);
        Task<Author> UpdateByIdAsync(int id, string name, DateTime dateOfBirth);
        Task<bool> DeleteByIdAsync(int id);
    }
}