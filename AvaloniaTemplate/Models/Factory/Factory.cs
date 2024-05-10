namespace AvaloniaTemplate.Models.Factory
{
    internal abstract class Factory
    {
        public abstract Animal Create();
        public abstract Animal Create(int i);
    }
}
