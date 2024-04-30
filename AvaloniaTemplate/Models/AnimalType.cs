using System.Collections.Generic;

namespace AvaloniaTemplate.Models
{
    public class AnimalType : Entity
    {
        public string Name { get; set; }
        public List<Amphibian> Amphibians { get; set; }
        public List<Bird> Birds { get; set; }
        public List<Mammal> Mammals { get; set; }

    }
}
