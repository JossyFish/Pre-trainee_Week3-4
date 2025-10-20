using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WK_34.Data;
using WK_34.Data.Interfaces;
using WK_34.DTOs;
using WK_34.Entities;
using WK_34.Models;
using WK_34.Repositories.Interfaces;

namespace WK_34.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private LibraryContext _context;

        public AuthorsRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors
               .Select(u => Author.Create(u.Id, u.Name, u.DateOfBirth))
               .ToListAsync();
        }

        public async Task<List<AuthorWithBooksCountDto>> GetAuthorsWithBooksAmountAsync()
        {
            return await _context.Authors
            .Include(a => a.Books)
            .Select(a => new AuthorWithBooksCountDto
            {
                Id = a.Id,
                Name = a.Name,
                DateOfBirth = a.DateOfBirth,
                BooksCount = a.Books.Count
            })
            .ToListAsync();
        }

        public async Task<Author> GetByNameAsync(string name)
        {
            ValidateName(name);

            return await _context.Authors
            .AsNoTracking()
            .Where(u => u.Name.Contains(name.Trim()))
            .Select(u => Author.Create(u.Id, u.Name, u.DateOfBirth))
            .FirstOrDefaultAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            ValidateId(id);

            return await _context.Authors
                .AsNoTracking()
                .Where(u => u.Id == id)
                .Select(u => Author.Create(u.Id, u.Name, u.DateOfBirth))
                .FirstOrDefaultAsync();
        }

        public async Task<Author> CreateAsync(string name, DateTime dateOfBirth)
        {
            ValidateName(name);
            ValidateBirthDate(dateOfBirth);

            var author = new AuthorEntity
            {
                Name = name.Trim(),
                DateOfBirth = dateOfBirth
            };

            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            return Author.Create(author.Id, author.Name, author.DateOfBirth);
        }

        public async Task<Author> UpdateByIdAsync(Author author)
        {
            ValidateId(author.Id);
            ValidateName(author.Name);
            ValidateBirthDate(author.DateOfBirth);

            var authorEntity = await GetEntityByIdAsync(author.Id);

            authorEntity.Name = author.Name.Trim();
            authorEntity.DateOfBirth = author.DateOfBirth;

            await _context.SaveChangesAsync();
            return Author.Create(author.Id, author.Name, author.DateOfBirth);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            ValidateId(id);

            var authorEntity = await GetEntityByIdAsync(id);

            _context.Remove(authorEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<AuthorEntity> GetEntityByIdAsync(int id)
        {
            var authorEntity = await _context.Authors
                .FirstOrDefaultAsync(u => u.Id == id);

            if (authorEntity == null)
            {
                throw new Exception($"Author not found");
            }
            return authorEntity;
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name.Trim()))
                throw new ArgumentException("Name must contain at least 1 character.");
        }

        private void ValidateId(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id cannot be less than 0.");
        }

        private void ValidateBirthDate(DateTime dateOfBirth)
        {
            if (dateOfBirth > DateTime.Now)
                throw new ArgumentException("Date of birth must be in the past.");
        }
    }
}
