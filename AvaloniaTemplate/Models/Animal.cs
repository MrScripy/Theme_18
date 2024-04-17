namespace AvaloniaTemplate.Models
{
    public abstract class Animal
    {
        public string? Name { get; set; }
        public string? LatName { get; set; }
        public abstract string? Type { get; }
    }
}
