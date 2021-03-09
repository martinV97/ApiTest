using Mapster;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;

namespace WebService.Infrastructure.Repositories
{
    public class PublicationService : IPublicationService
    {
        private readonly IPublicationRepository _publicationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public PublicationService(IPublicationRepository publicationRepository, IUserRepository userRepository, IProductRepository productRepository)
        {
            _publicationRepository = publicationRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task<PublicationDTO> CreatePublication(Publication publication)
        {
            if (await _userRepository.GetUserById(publication.UserId) is null)
                throw new ArgumentNullException($"The user: {publication.UserId} it doesn't exist");
            if (await _productRepository.GetProductById(publication.ProductId) is null)
                throw new ArgumentNullException($"The product: {publication.ProductId} it doesn't exist");
            return await _publicationRepository.CreatePublication(publication.Adapt<PublicationDTO>());
        }

        public async Task<List<PublicationDTO>> GetAllPublicationsByProductId(string id)
        {
            return await _publicationRepository.GetAllPublicationsByProductId(id);
        }

        public async Task<List<PublicationDTO>> GetAllPublicationsByUserId(string id)
        {
            return await _publicationRepository.GetAllPublicationsByUserId(id);
        }

        public async Task<PublicationDTO> GetPublicationById(string id)
        {
            return await _publicationRepository.GetPublicationById(id);
        }

        public async Task<List<PublicationDTO>> GetPublicationsByDate(DateTime startDate)
        {
            return await _publicationRepository.GetPublicationsByDate(startDate);
        }

        public async Task<PublicationDTO> UpdatePublicationById(PublicationDTO publication)
        {
            if (await _userRepository.GetUserById(publication.UserId) is null)
                throw new ArgumentNullException($"The user: {publication.UserId} it doesn't exist");
            if (await _productRepository.GetProductById(publication.ProductId) is null)
                throw new ArgumentNullException($"The product: {publication.ProductId} it doesn't exist");
            return await _publicationRepository.UpdatePublicationById(publication);
        }

        public async Task<PublicationDTO> DeletePublicationById(string id)
        {
            return await _publicationRepository.DeletePublicationById(id);
        }
    }
}
