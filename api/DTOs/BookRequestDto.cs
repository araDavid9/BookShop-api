namespace api.DTOs;

public class BookRequestDto
{
    public required BookDto Book { get; set; }
    public required AuthorDto Author { get; set; }
    public required PublisherDto Publisher { get; set; }
}