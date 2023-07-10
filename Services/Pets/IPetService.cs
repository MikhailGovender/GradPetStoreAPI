using Microsoft.AspNetCore.JsonPatch;

namespace WebAPIv1
{
    public interface IPetService
    {
        //Works with Models Only

        //Function: Insert Pet To DB
        void CreatePet(Pet PetDBO);
        Task<Pet> GetPetById(Guid id);
        void UpsertPet(Pet upsertPet);
        void PatchPet(Guid id, Pet patchPet);
        void DeletePet(Guid id);
        Task<List<Pet>> GetPetByStatus(string status);
    }
}