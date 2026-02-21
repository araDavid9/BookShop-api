using api.Models;

namespace api.Repositories.Interfaces;

public interface IPublisherRepository
{
    public Task<Publisher?> GetPublisherByName(string name);
    public Task<Publisher> CreatePublisher(Publisher publisher);
    public Task<Publisher> GetPublisherById(string publisherId);
}