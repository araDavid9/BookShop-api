using api.DTOs;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }
    
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _bookService.GetAllBooks();
        return books == null 
            ? NotFound("There is no available books!") 
            : Ok(books);
    }
    [HttpGet("{bookName}")]
    public async Task<IActionResult> GetBook(string bookName)
    {
        var currentBook = await _bookService.GetBookByName(bookName);
        return currentBook != null 
            ? Ok(currentBook)
            : NotFound("There is no current book with the given book name");
    }

    [HttpPost("Create")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateBook([FromBody] BookRequestDto bookRequestDto)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var newBook = await _bookService.CreateNewBook(bookRequestDto);
        return Ok($"Book has created:{newBook.Id}");
    }

    [HttpGet("Delete/{bookId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBook(string bookId)
    {
        var deletedBook = await _bookService.Delete(bookId);
        return deletedBook == null
            ? NotFound("There is no current book with the given book id")
            : Ok(deletedBook);
    }

    [HttpPost("Update/{bookId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBook([FromBody] BookRequestDto book, string bookId)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _bookService.UpdateBook(book, bookId);
        return Ok(result);
        
       
    }
}