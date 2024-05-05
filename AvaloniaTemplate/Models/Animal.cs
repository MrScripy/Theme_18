using System.ComponentModel.DataAnnotations.Schema;

namespace AvaloniaTemplate.Models
{
    public abstract class Animal : Entity, IAnimal
    {
        public string? Name { get; set; }
        public string? LatName { get; set; }
        public abstract string? Type { get; }

        public int AnimalTypeId { get; set; }
        [ForeignKey("AnimalTypeId")]
        public AnimalType AnimalType { get; set; }
    }
}
