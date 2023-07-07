namespace WebAPIv1
{
    public record PetResponse(
        Guid Id,
        string Name,
        string AnimalType,
        string Breed,
        string Status,
        int Age,
        string[] ImageURLS
        );
}