using api.Data;
using api.Models;
using api.Repositories.Interfaces;

namespace api.Repositories;

public class PublisherRepository : IPublisherRepository 
{
    private readonly BookStoreDbContext _dbContext;

    public PublisherRepository(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Publisher?> GetPublisherByName(string name)
    {
        try
        {
            return  _dbContext.Publishers.FirstOrDefault(x=>x.Name == name);
        }
        catch (Exception e)
        {
            return null;
        }
       
    }

    public async Task<Publisher> CreatePublisher(Publisher publisher)
    {
        _dbContext.Publishers.Add(publisher);
        await _dbContext.SaveChangesAsync();
        return publisher;
    }

    public async Task<Publisher> GetPublisherById(string publisherId)
    {
        return _dbContext.Publishers.FirstOrDefault(x=> x.id == publisherId);
    }
}