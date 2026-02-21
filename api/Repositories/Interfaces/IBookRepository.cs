using api.DTOs;
using api.Models;

namespace api.Repositories.Interfaces;

public interface IBookRepository
{
    public Task<List<Book>> GetAllBooks();
    public Task<Book?> GetBookByName(string name);
    public Task<Book> CreateNewBook(Book book);
    public Task<Book?> DeleteBook(string bookId);
    public Task<Book?> UpdateBook(BookRequestDto bookDto,string bookId);
    
    public Task<Book> GetBookById(string bookId);
    public Task UpdateBookStock(int stockToDecrease, string bookId);
}