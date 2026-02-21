namespace api.DTOs;

public class BookOverviewDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public AuthorDto Author { get; set; }
    public PublisherDto Publisher { get; set; }
}