using AvaloniaTemplate.Stores.Db;

namespace AvaloniaTemplate.Models
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
