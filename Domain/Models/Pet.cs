using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace WebAPIv1
{
    public class Pet
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string AnimalType { get; set; }
        public string Breed { get; set; }
        public string Status { get; set; }
        public int Age { get; set; }
        public string[] ImageURLS { get; set; }

        public Pet(Guid id, string name, string animalType, string breed, string status, int age, string[] imageURLS)
        {
            //Enforve invariants
            Id = id;
            Name = name;
            AnimalType = animalType;
            Breed = breed;
            Status = status;
            Age = age;
            ImageURLS = imageURLS;
        }

    }
}