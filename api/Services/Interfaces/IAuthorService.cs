using api.DTOs;
using api.Models;

namespace api.Services.Interfaces;

public interface IAuthorService
{
    public Task<Author> CreateOrGetAuthor(AuthorDto authorDto);
    public Task<AuthorDto> GetAuthorById(string authorId);
}