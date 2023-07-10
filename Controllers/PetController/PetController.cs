using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
                request.Status.ToUpper(),
                request.Age
                );

            //TO DO : Insert Into DB
            _petService.CreatePet(newPet);

            //Map Model Back To Response
            PetResponse response = new PetResponse(
                newPet.Id,
                newPet.Name,
                newPet.AnimalType,
                newPet.Breed,
                newPet.Status,
                newPet.Age);


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
                    retrievedPet.Age);

                return Ok(response);
            }

            return NotFound();
        }

        [HttpGet("~/Pets/{status}")]
        public IActionResult GetPetsByStatus(string status)
        {
            //Retrieve Pet By Guid
            List<Pet> retrievedPet = _petService.GetPetByStatus(status.ToUpper());

            return Ok(retrievedPet);
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
                request.Status.ToUpper(),
                request.Age
                );

            _petService.UpsertPet(UpsertPet);

            return Ok(request);
        }

        [HttpDelete(Name = "{id:guid}")]
        public IActionResult DeletePet(Guid id)
        {
            _petService.DeletePet(id);
            return NoContent();
        }

        [HttpPost("/Image")]
        public IActionResult UploadImage(Guid id, IFormFile file)
        {
            Pet? retrievedPet = _petService.GetPetById(id);
            string uniqueFileName = (id.ToString() + "-" + file.FileName.Split('.')[0] + "-" + DateTime.Now.ToString("yyyy-MM-dd")).ToString()+ "." + file.FileName.Split('.')[1];

            if (retrievedPet is not null)
            {
                if (ValidateFile(file))
                {
                    try
                    {
                        var tempUploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "TempUploads\\Pets");
                        var exactPath = Path.Combine(Directory.GetCurrentDirectory(), "TempUploads\\Pets", uniqueFileName);
                        using (var stream = new FileStream(exactPath, FileMode.Create))
                        {
                             file.CopyTo(stream);
                        }
                    }catch (Exception)
                    {
                        return Problem();
                    }
                }
            }
            return Ok(uniqueFileName);
        }

        private string[] permittedExtensions = { ".png", ".jpg" };
        private bool ValidateFile(IFormFile file)
        {
            string ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!string.IsNullOrEmpty(ext) || permittedExtensions.Contains(ext))
            {
                return true;
            }

            return false;
        }

    }}