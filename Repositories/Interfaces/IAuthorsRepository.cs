using WK_34.DTOs;
using WK_34.Models;

namespace WK_34.Repositories.Interfaces
{
    public interface IAuthorsRepository
    {
        Task<List<Author>> GetAllAsync();
        Task<List<AuthorWithBooksCountDto>> GetAuthorsWithBooksAmountAsync();
        Task<Author> GetByIdAsync(int id);
        Task<Author> GetByNameAsync(string name);
        Task<Author> CreateAsync(string name, DateTime dateOfBirth);
        Task<Author> UpdateByIdAsync(Author author);
        Task<bool> DeleteByIdAsync(int id);
    }
}