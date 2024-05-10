namespace AvaloniaTemplate.Models.Factory
{
    internal class AmphibianFactory : Factory
    {
        public override Animal Create() => new Amphibian();

        public override Animal Create(int i)
        {
            return new Amphibian()
            { 
                Name = $"Name {i.ToString()}",
                LatName = $"LatName {i.ToString()}"
            };
        }
    }
}
