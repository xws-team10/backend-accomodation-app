﻿using AutoMapper;
using Grpc.Core;
using reservation_service;
using search_service.DTO;
using search_service.Model;
using search_service.Repository;
using search_service.Repository.Core;
using System;

namespace search_service.ProtoServices
{
    public class GrpcCreateService : GrpcCreate.GrpcCreateBase
    {
        private readonly AccomodationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public GrpcCreateService(AccomodationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public override async Task<CreateResponse> CreateNewAccomodation(CreateRequest request, ServerCallContext context)
        {
            var response = new CreateResponse();
            Accomodation accomodation = new Accomodation();
            accomodation.Id = Guid.Parse(request.Id);
            accomodation.Name = request.Name;
            accomodation.Description = request.Description;
            accomodation.Price = request.Price;
            accomodation.Capacity = request.Capacity;
            accomodation.Address = new Address(request.Country, request.City, request.Street, request.StreetNumber);
            accomodation.AvailableFromDate = DateTime.Parse(request.AvailableFromDate);
            accomodation.AvailableToDate = DateTime.Parse(request.AvailableToDate);
            try
            {
                _reservationRepository.CreateAsync(accomodation).Wait();
                response.Success = true;
                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                return await Task.FromResult(response);
            } 
        }

        public override async Task<UpdateResponse> UpdateAccomodation(UpdateRequest request, ServerCallContext context)
        {
            var response = new UpdateResponse();
            AccomodationUpdateDto accomodationUpdateDto = new AccomodationUpdateDto();
            accomodationUpdateDto.Id = Guid.Parse(request.Id);
            accomodationUpdateDto.AvailableFromDate = DateTime.Parse(request.AvailableFromDate);
            accomodationUpdateDto.AvailableToDate = DateTime.Parse(request.AvailableToDate);
            try
            {
                _reservationRepository.AccomodationUpdate(accomodationUpdateDto).Wait();
                response.Success = true;
                return await Task.FromResult(response);
            }
            catch(Exception ex)
            {
                response.Success = false;
                return await Task.FromResult(response);
            }
            

            return null;
        }

        public override Task<UpdatePriceResponse> UpdateAccomodationPrice(UpdatePriceRequest request, ServerCallContext context)
        {
            AccomodationChangePriceDto changePriceDto = new AccomodationChangePriceDto();
            changePriceDto.Id = Guid.Parse(request.Id);
            changePriceDto.Price = request.Price;

            _reservationRepository.AccomodationChangePrice(changePriceDto).Wait();
            return null;
        }
    }
}
