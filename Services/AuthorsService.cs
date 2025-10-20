using Azure.Core;
using System.ComponentModel.DataAnnotations;
using WK_34.Data.Interfaces;
using WK_34.DTOs;
using WK_34.Models;
using WK_34.Repositories;
using WK_34.Repositories.Interfaces;
using WK_34.Services.Interfaces;

namespace WK_34.Services
{
    public class AuthorsService : IAuthorsService
    {
        IAuthorsRepository _authorsRepository;
        IAuthorValidator _validator;
        public AuthorsService(IAuthorsRepository authorsRepository, IAuthorValidator validator) {
            _authorsRepository = authorsRepository;
            _validator = validator;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _authorsRepository.GetAllAsync();
        }

        public async Task<List<AuthorWithBooksCountDto>> GetAuthorsWithBooksAmountAsync()
        {
            return await _authorsRepository.GetAuthorsWithBooksAmountAsync();
        }
        public async Task<Author> GetByNameAsync(string name)
        {
            _validator.ValidateName(name);

            return await _authorsRepository.GetByNameAsync(name);
        }
        public async Task<Author> GetByIdAsync(int id)
        {
            _validator.ValidateId(id);

            return await _authorsRepository.GetByIdAsync(id);
        }
        public async Task<Author> CreateAsync(string name, DateTime dateOfBirth)
        {
            _validator.ValidateName(name);
            _validator.ValidateDateOfBirth(dateOfBirth);

            var authorCheck = await _authorsRepository.GetByNameAsync(name);

            if (authorCheck != null)
                throw new InvalidOperationException($"Author already exists");

            return await _authorsRepository.CreateAsync(name, dateOfBirth);
        }

        public async Task<Author> UpdateByIdAsync(int id, string name, DateTime dateOfBirth)
        {
            _validator.ValidateId(id);
            _validator.ValidateName(name);
            _validator.ValidateDateOfBirth(dateOfBirth);

            var author = Author.Create(id, name, dateOfBirth);

            return await _authorsRepository.UpdateByIdAsync(author);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            _validator.ValidateId(id);

            return await (_authorsRepository.DeleteByIdAsync(id));
        }
    }
}
