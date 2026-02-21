using api.DTOs;
using api.Models;
using api.Repositories.Interfaces;
using api.Services.Interfaces;

namespace api.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorService _authorService;
    private readonly IPublisherService _publisherService;
    
    public BookService(IBookRepository bookRepository, IAuthorService authorService, IPublisherService publisherService)
    {
    {
        _bookRepository = bookRepository;
        _authorService = authorService;
        _publisherService = publisherService;
       
    }}

    public async Task<List<BookOverviewDto>> GetAllBooks()
    {
        var books = await _bookRepository.GetAllBooks();

        if (books.Count == 0)
            return new List<BookOverviewDto>();

        var tasks = books
            .Where(x => x.DeletedAt == null)
            .Select(async x => new BookOverviewDto
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock,
                Author = await _authorService.GetAuthorById(x.AuthorId),
                Publisher = await _publisherService.GetPublisherById(x.PublisherId)
            });

        return (await Task.WhenAll(tasks)).ToList();
    }

    public async Task<BookOverviewDto?> GetBookByName(string bookName)
    {
        
        var currentBook = await _bookRepository.GetBookByName(bookName);
        return currentBook == null 
            ? null
            : new()
            {
                Id = currentBook.Id,
                Name = currentBook.Name,
                Price = currentBook.Price,
                Stock = currentBook.Stock,
                Author = await _authorService.GetAuthorById(currentBook.AuthorId),
                Publisher = await _publisherService.GetPublisherById(currentBook.PublisherId)
            };
    }

    public async Task<Book> CreateNewBook(BookRequestDto bookRequestDto)
    {
        var existingBook = await _bookRepository.GetBookByName(bookRequestDto.Book.Name);
        if (existingBook != null)
            return null;
        
        var author = await _authorService.CreateOrGetAuthor(bookRequestDto.Author);
        var publisher = await _publisherService.CreateOrGetPublisher(bookRequestDto.Publisher);
        var newBook = new Book()
        {
            Name = bookRequestDto.Book.Name,
            Price = bookRequestDto.Book.Price,
            Stock = bookRequestDto.Book.Stock,
            AuthorId = author.Id,
            PublisherId = publisher.id
        };
        await _bookRepository.CreateNewBook(newBook);
        return newBook;
    }

    public async Task<Book?> Delete(string bookId)
    {
        return await _bookRepository.DeleteBook(bookId);
    }

    public async Task<Book?> UpdateBook(BookRequestDto bookDto ,string bookId)
    {
        
        return await _bookRepository.UpdateBook(bookDto, bookId);
    }
    
}