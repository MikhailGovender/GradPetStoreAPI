namespace WebAPIv1
{
    public record UpsertPetRequest(
        //Exlude ID & AutoGen Fields
        string Name,
        string AnimalType,
        string Breed,
        string Status,
        int Age,
        string[] ImageURLS
        );
}