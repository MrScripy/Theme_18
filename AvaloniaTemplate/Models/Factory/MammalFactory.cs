namespace AvaloniaTemplate.Models.Factory
{
    internal class MammalFactory : Factory
    {
        public override Animal Create() => new Mammal();

        public override Animal Create(int i)
        {
            return new Mammal()
            {
                Name = $"Name {i.ToString()}",
                LatName = $"LatName {i.ToString()}"
            };
        }
    }
}
