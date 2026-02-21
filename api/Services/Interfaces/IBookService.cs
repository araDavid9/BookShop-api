using api.DTOs;
using api.Models;

namespace api.Services.Interfaces;

public interface IBookService
{
    public Task<List<BookOverviewDto>> GetAllBooks();
    public Task<BookOverviewDto?> GetBookByName(string name);
    public Task<Book> CreateNewBook(BookRequestDto bookRequestDto);
    public Task<Book?> Delete(string bookId);
    public Task<Book?> UpdateBook(BookRequestDto bookDto, string bookId);
}