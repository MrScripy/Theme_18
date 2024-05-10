namespace AvaloniaTemplate.Models
{
    public interface IAnimal : IEntity
    {
        AnimalType AnimalType { get; set; }
        int AnimalTypeId { get; set; }
        string? LatName { get; set; }
        string? Name { get; set; }
        string? Type { get; }
    }
}