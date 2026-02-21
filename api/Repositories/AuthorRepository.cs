using api.Data;
using api.DTOs;
using api.Models;
using api.Repositories.Interfaces;

namespace api.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly BookStoreDbContext _dbContext;

    public AuthorRepository(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Author?> GetAuthor(AuthorDto authorDto)
    {
        try
        {
            // it possible to have 2 authors with same name 
            return _dbContext.Authors
                .FirstOrDefault(x=>x.Name == authorDto.Name && x.Country == authorDto.Country && x.Age == authorDto.Age);
        }
        catch (Exception e)
        {
            return null;
        }
       
    }

    public async Task<Author> CreateAuthor(Author author)
    {
        _dbContext.Authors.Add(author);
        await _dbContext.SaveChangesAsync();
        return author;
    }

    public async Task<Author> GetAuthorById(string authorId)
    {
        return _dbContext.Authors.FirstOrDefault(x => x.Id == authorId);
    }
}