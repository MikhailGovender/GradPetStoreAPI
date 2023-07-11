using Dapper;
using Microsoft.AspNetCore.JsonPatch;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace WebAPIv1
{
    public class PetService : IPetService
    {
        //Temporary In-Memory Store
        //private static readonly Dictionary<Guid, Pet> _petDictionary = new();
        private readonly IConfiguration _config;
        public PetService(IConfiguration config)
        {
            _config = config;
        }

        public async void CreatePet(Pet PetDBO)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(
                "INSERT INTO Pets(Id, Name, AnimalType, Breed, Status, Age)" +
                "VALUES (@Id, @Name, @AnimalType, @Breed, @Status, @Age)", PetDBO);
            //_petDictionary.Add(PetDBO.Id, PetDBO);
        }

        public async void DeletePet(Guid id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("DELETE FROM Pets Where Id = @Id", new { Id = id });
            //_petDictionary.Remove(id);
        }

        //TO DO: REFACTOR TO RETURN EMPTY PET
        public async Task<Pet> GetPetById(Guid id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return await connection.QueryFirstAsync<Pet>("SELECT * FROM Pets WHERE Id = @Id", new {Id = id});
           //_petDictionary.TryGetValue(id, out Pet? value) ? value : null;
        }

        public async Task<List<Pet>> GetPetByStatus(string status)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return (await connection.QueryAsync<Pet>("SELECT * FROM Pets WHERE Status = @Status", new { Status = status })).ToList();
        }

        public async void PatchPet(Guid id, Pet patchPet)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("UPDATE Pets" +
                " SET Name = @Name, AnimalType = @AnimalTypr, Breed = @Breed, Status = @Status, Age = @Age  WHERE Id = @Id", patchPet);
        }

        public void UploadImage(Guid id, string uniqueFileName, string exactPath)
        {
            throw new NotImplementedException();
        }

        public async void UploadImage(Guid id, string uniqueFileName, byte[] filebytes)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync("INSERT INTO PetImages VALUES (@Id, @FileName, @File)", new { Id = id, FileName = uniqueFileName, File = filebytes });
        }

        public async void UpsertPet(Pet upsertPet)
        {
            Pet? retrievedPet = await GetPetById(upsertPet.Id);
            if(retrievedPet is not null)
            {
                using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                await connection.ExecuteAsync("UPDATE Pets" +
                    " SET Name = @Name, AnimalType = @AnimalTypr, Breed = @Breed, Status = @Status, Age = @Age  WHERE Id = @Id", upsertPet);
            }
            else
            {
                CreatePet(upsertPet);
            }
        }
    }
}