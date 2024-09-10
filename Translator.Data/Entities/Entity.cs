namespace Translator.Data.Entities
{
    public class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
    }
}
