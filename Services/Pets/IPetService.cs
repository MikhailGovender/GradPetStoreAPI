using Microsoft.AspNetCore.JsonPatch;

namespace WebAPIv1
{
    public interface IPetService
    {
        //Works with Models Only

        //Function: Insert Pet To DB
        void CreatePet(Pet PetDBO);
        Pet? GetPetById(Guid id);
        void UpsertPet(Pet upsertPet);
        void PatchPet(Guid id, JsonPatchDocument<Pet> patchPet);
        void DeletePet(Guid id);
        List<Pet> GetPetByStatus(string status);
    }
}