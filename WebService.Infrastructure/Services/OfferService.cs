using Mapster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Enumerations;
using WebService.Core.Interfaces;

namespace WebService.Infrastructure.Repositories
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPublicationRepository _publicationRepository;
        private readonly IPaymentRepository _paymentRepository;

        public OfferService(IOfferRepository offerRepository, IUserRepository userRepository, IPublicationRepository publicationRepository, IPaymentRepository paymentRepository)
        {
            _offerRepository = offerRepository;
            _userRepository = userRepository;
            _publicationRepository = publicationRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<OfferDTO> CreateOffer(Offer offer)
        {
            if (await _userRepository.GetUserById(offer.UserId) is null)
                throw new ArgumentNullException($"The user: {offer.UserId} it doesn't exist");
            if (await _publicationRepository.GetPublicationById(offer.PublicationId) is null)
                throw new ArgumentNullException($"The publication: {offer.PublicationId} it doesn't exist");
            return await _offerRepository.CreateOffer(offer.Adapt<OfferDTO>());
        }

        public async Task<List<OfferDTO>> GetAllOffersByPublicationId(string id)
        {
            return await _offerRepository.GetAllOffersByPublicationId(id);
        }

        public async Task<List<OfferDTO>> GetAllOffersByUserId(string id)
        {
            return await _offerRepository.GetAllOffersByUserId(id);
        }

        public async Task<OfferDTO> GetOfferById(string id)
        {
            return await _offerRepository.GetOfferById(id);
        }

        public async Task<OfferDTO> UpdateOfferById(OfferDTO offer)
        {
            if (await _userRepository.GetUserById(offer.UserId) is null)
                throw new ArgumentNullException($"The user: {offer.UserId} it doesn't exist");
            if (await _publicationRepository.GetPublicationById(offer.PublicationId) is null)
                throw new ArgumentNullException($"The publication: {offer.PublicationId} it doesn't exist");
            if(offer.State == OfferStateEnum.accepted)
            {
                if(String.IsNullOrEmpty(offer.PaymentId))
                    throw new ArgumentNullException($"The payment can't be null");
                if(await _paymentRepository.GetPaymentById(offer.PaymentId) is null)
                    throw new ArgumentNullException($"The payment: {offer.PaymentId} it doesn't exist");
            }
            return await _offerRepository.UpdateOfferById(offer);
        }

        public async Task<OfferDTO> DeleteOfferById(string id)
        {
            return await _offerRepository.DeleteOfferById(id);
        }
    }
}
