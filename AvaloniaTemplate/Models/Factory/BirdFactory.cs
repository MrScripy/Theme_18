namespace AvaloniaTemplate.Models.Factory
{
    internal class BirdFactory : Factory
    {
        public override Animal Create() => new Bird();

        public override Animal Create(int i)
        {
            return new Bird()
            {
                Name = $"Name {i.ToString()}",
                LatName = $"LatName {i.ToString()}"
            };
        }
    }
}
