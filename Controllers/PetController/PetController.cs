using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Reflection;

namespace WebAPIv1.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PetController : ControllerBase
    {
        //Use Dependency Injection To Add Our DB Class
        private readonly IPetService _petService;
        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpPost()]
        public IActionResult CreatePet(CreatePetRequest request)
        {
            //Map Request To Model
            Pet newPet = new Pet(
                Guid.NewGuid(),
                request.Name,
                request.AnimalType,
                request.Breed,
                request.Status,
                request.Age,
                request.ImageURLS);

            //TO DO : Insert Into DB
            _petService.CreatePet(newPet);

            //Map Model Back To Response
            PetResponse response = new PetResponse(
                newPet.Id,
                newPet.Name,
                newPet.AnimalType,
                newPet.Breed,
                newPet.Status,
                newPet.Age,
                newPet.ImageURLS);


            return CreatedAtAction(
                actionName: nameof(GetPet),
                routeValues: new { id = newPet.Id },
                value: response);
        }        
        
        [HttpGet(Name = "{id:guid}")]
        public IActionResult GetPet(Guid id)
        {
            //Retrieve Pet By Guid
            Pet? retrievedPet = _petService.GetPetById(id);
            if (retrievedPet is not null)
            {
                //Map to Response Obj
                PetResponse response = new(
                    retrievedPet.Id,
                    retrievedPet.Name,
                    retrievedPet.AnimalType,
                    retrievedPet.Breed,
                    retrievedPet.Status,
                    retrievedPet.Age,
                    retrievedPet.ImageURLS);

                return Ok(response);
            }

            return NotFound();
        }

        [HttpPatch(Name = "{id:guid}")]
        public IActionResult PatchPet(Guid id, JsonPatchDocument<Pet> request)
        {
            _petService.PatchPet(id, request);
            return Ok(request);
        }

        [HttpPut(Name = "{id:guid}")]
        public IActionResult UpsertPet(Guid id, UpsertPetRequest request)
        {
            Pet UpsertPet = new(
                id,
                request.Name,
                request.AnimalType,
                request.Breed,
                request.Status,
                request.Age,
                request.ImageURLS);

            _petService.UpsertPet(UpsertPet);

            return Ok(request);
        }

        [HttpDelete(Name = "{id:guid}")]
        public IActionResult DeletePet(Guid id)
        {
            _petService.DeletePet(id);
            return NoContent();
        }


    }}