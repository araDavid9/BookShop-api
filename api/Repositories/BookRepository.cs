using api.Data;
using api.DTOs;
using api.Models;
using api.Repositories.Interfaces;

namespace api.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ILogger<BookRepository> _logger;
    private readonly BookStoreDbContext _dbContext;
    
    public BookRepository(ILogger<BookRepository> logger, BookStoreDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<List<Book>> GetAllBooks()
    {
        return _dbContext.Books.ToList();
    }
    public async Task<Book?> GetBookByName(string name)
    {
        try
        {
            return _dbContext.Books.FirstOrDefault(x => x.Name == name);
        }
        catch (Exception ex)
        {
            return null;
        }
        
    }

    public async Task<Book> CreateNewBook(Book book)
    {
        _dbContext.Books.Add(book);
        await _dbContext.SaveChangesAsync();
        return book;
    }

    public async Task<Book?> DeleteBook(string bookId)
    {
        var book = _dbContext.Books.FirstOrDefault(b => b.Id == bookId);
        if (book == null)
        {
          // throw
        }
        
        book.DeletedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
        return book;
    }

    public async Task<Book?> UpdateBook(BookRequestDto bookDto,string bookId)
    {
        var currentBook = _dbContext.Books.FirstOrDefault(x => x.Id == bookId);
        if (currentBook == null)
            return null;
        
        currentBook.Name = bookDto.Book.Name;
        currentBook.Price = bookDto.Book.Price;
        currentBook.Stock = bookDto.Book.Stock;

        await _dbContext.SaveChangesAsync();
        return currentBook;
    }

    public async Task<Book> GetBookById(string bookId)
    {
        return _dbContext.Books.FirstOrDefault(x => x.Id == bookId);
    }

    public async Task UpdateBookStock(int stockToDecrease, string bookId)
    {
        var currentBook = _dbContext.Books.FirstOrDefault(x => x.Id == bookId);
        currentBook.Stock -= stockToDecrease;
        await _dbContext.SaveChangesAsync();
        
    }
    
}