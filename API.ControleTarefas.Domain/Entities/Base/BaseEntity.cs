namespace API.ControleTarefas.Domain.Entities.Base
{
    public class BaseEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime? CreatedAt { get; private set; } 
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public bool IsInactive { get; private set; } = false;

        public void SetCreationDate()
        {
            if (CreatedAt == null)
            {
                CreatedAt = DateTime.UtcNow;
            }
        }

        public void SetUpdateDate()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDeleteDate()
        {
            DeletedAt = DateTime.UtcNow;
        }
        public void SetInactive()
        {
            IsInactive = true;
        }
    }
}
