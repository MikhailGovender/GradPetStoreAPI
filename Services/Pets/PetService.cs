using Microsoft.AspNetCore.JsonPatch;
using System.Reflection.Metadata.Ecma335;

namespace WebAPIv1
{
    public class PetService : IPetService
    {
        //Temporary In-Memory Store
        private static readonly Dictionary<Guid, Pet> _petDictionary = new();
        public void CreatePet(Pet PetDBO)
        {
            _petDictionary.Add(PetDBO.Id, PetDBO);
        }

        public void DeletePet(Guid id)
        {
            _petDictionary.Remove(id);
        }

        public Pet? GetPetById(Guid id)
        {
            return _petDictionary.TryGetValue(id, out Pet? value) ? value : null;
        }

        public void PatchPet(Guid id, JsonPatchDocument<Pet> patchPet)
        {
            Pet? retrievedPet = GetPetById(id);
            if(retrievedPet is not null)
            {
                patchPet.ApplyTo(retrievedPet);
                _petDictionary[id] = retrievedPet;
            }
        }

        public void UpsertPet(Pet upsertPet)
        {
            Pet? retrievedPet = GetPetById(upsertPet.Id);
            if(retrievedPet is not null)
            {
                _petDictionary[upsertPet.Id] = upsertPet;
            }
            else
            {
                CreatePet(upsertPet);
            }
        }
    }
}