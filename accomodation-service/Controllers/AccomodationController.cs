using Microsoft.AspNetCore.Mvc;
using accomodation_service.Model;
using accomodation_service.Service;
using AutoMapper;
using accomodation_service.Dtos;
using accomodation_service.ProtoServices;

namespace accomodation_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccomodationController : ControllerBase
    {
        private readonly AccomodationService _service;
        private readonly IMapper _mapper;
        private readonly CreateAccomodation createAccomodation;

        public AccomodationController(AccomodationService service, IMapper mapper, CreateAccomodation createAccomodation)
        {
            _service = service;
            _mapper = mapper;
            this.createAccomodation = createAccomodation;
        }

        [HttpGet]
        public async Task<IEnumerable<AccomodationReadDto>> GetAccomodations(){
            var accomodations = await _service.GetAllAsync();
            // var accomodationsReadDto = _mapper.Map<IEnumerable<AccomodationReadDto>>(accomodations);
            return _mapper.Map<IEnumerable<AccomodationReadDto>>(accomodations);  
        }

        [HttpGet("{id}", Name = "GetAccomodationById")]
        public async Task<ActionResult<AccomodationReadDto>> GetAccomodationById(Guid id)
        {
            var accomodationItem = await _service.GetAccomodationById(id);
            if (accomodationItem != null)
            {
                return Ok(_mapper.Map<AccomodationReadDto>(accomodationItem));
            }

            return NotFound();
        }

        [HttpGet("availabilityCheck/{id}")]
        public async Task<bool> AvailabilityCheck(Guid id, DateTime from, DateTime to){
            return await _service.AvailabilityCheck(id, from, to);
        }  

        [HttpPost]
        public async Task<ActionResult<AccomodationReadDto>> CreateAsync(AccomodationCreateDto accomodationCreateDto)
        {
            var accomodationModel = _mapper.Map<Accomodation>(accomodationCreateDto);

            

            if (!accomodationModel.AvailabilityInitialValidate()) return BadRequest(new ProblemDetails{Title = "Date time is not valid!"});
            {
                await _service.CreateAsync(accomodationModel);
                createAccomodation.CreateNewAccomodation(accomodationModel.Id, accomodationModel.Name, accomodationModel.Description, accomodationModel.Price, accomodationModel.MaxCapacity, accomodationModel.Address.Country, accomodationModel.Address.City,
                accomodationModel.Address.Street, accomodationModel.Address.StreetNumber, accomodationModel.AvailableFromDate, accomodationModel.AvailableToDate);
            }

            var accomodationReadDto = _mapper.Map<AccomodationReadDto>(accomodationModel);
            
            return CreatedAtRoute(nameof(GetAccomodationById), new { id = accomodationReadDto.Id}, accomodationReadDto);
            // await _service.CreateAsync(newAccomodation);
            // return CreatedAtAction(nameof(GetAccomodations), new { id = newAccomodation.Id }, newAccomodation);
        }

        [HttpPut]
        public async Task<IActionResult> AccomodationUpdate(AccomodationChangeDto accomodationChangeDto)
        {
            await _service.AccomodationUpdate(accomodationChangeDto);
            createAccomodation.UpdateAccomodation(accomodationChangeDto.Id,accomodationChangeDto.AvailableFromDate,accomodationChangeDto.AvailableToDate);
            return Ok();
            
        }
    }

}