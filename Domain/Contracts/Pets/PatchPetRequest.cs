namespace WebAPIv1.Domain.Contracts.Pets
{
    public record UpsertPetRequest(
        string Name,
        string AnimalType,
        string Breed,
        string Status,
        int Age,
        string[] ImageURLS
        );
}
