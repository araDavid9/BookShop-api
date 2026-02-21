using api.DTOs;
using api.Models;
using api.Repositories.Interfaces;
using api.Services.Interfaces;

namespace api.Services;

public class AuthorService: IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }
    public async Task<Author> CreateOrGetAuthor(AuthorDto authorDto)
    {
        var author = await _authorRepository.GetAuthor(authorDto);
        if(author != null)
            return author;

        author = new Author
        {
            Name = authorDto.Name,
            Age = authorDto.Age,
            Bio = authorDto.Bio,
            Country = authorDto.Country
        };
        
        return await _authorRepository.CreateAuthor(author);
    }

    public async Task<AuthorDto> GetAuthorById(string authorId)
    {
        var result = await _authorRepository.GetAuthorById(authorId);
        return new AuthorDto
        {
            Name = result.Name,
            Age = result.Age,
            Bio = result.Bio,
            Country = result.Country
        };
    }
}
