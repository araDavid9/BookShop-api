using api.DTOs;
using api.Models;
using api.Repositories.Interfaces;
using api.Services.Interfaces;

namespace api.Services;

public class PublisherService: IPublisherService
{
    private readonly IPublisherRepository _publisherRepository;

    public PublisherService(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }
    
    public async Task<Publisher> CreateOrGetPublisher(PublisherDto publisherDto)
    {
        var publisher = await _publisherRepository.GetPublisherByName(publisherDto.Name);
        if(publisher != null)
            return publisher;

        publisher = new Publisher()
        {
            Name = publisherDto.Name,
            Country = publisherDto.Country
        };
        
        return await _publisherRepository.CreatePublisher(publisher);
    }

    public async Task<PublisherDto> GetPublisherById(string publisherId)
    {
        var result = await _publisherRepository.GetPublisherById(publisherId);
        return new PublisherDto
        {
            Name = result.Name,
            Country = result.Country
        };
    }
}