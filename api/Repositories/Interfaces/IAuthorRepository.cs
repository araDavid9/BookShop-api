using api.Models;
using api.DTOs;
namespace api.Repositories.Interfaces;

public interface IAuthorRepository
{
    public Task<Author?> GetAuthor(AuthorDto authorDto);
    public Task<Author> CreateAuthor(Author author);
    public Task<Author> GetAuthorById(string authorId);
}