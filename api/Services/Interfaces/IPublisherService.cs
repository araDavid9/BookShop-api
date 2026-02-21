using api.DTOs;
using api.Models;

namespace api.Services.Interfaces;

public interface IPublisherService
{
    public Task<Publisher> CreateOrGetPublisher(PublisherDto publisherDto);
    public Task<PublisherDto> GetPublisherById(string publisherId);
}