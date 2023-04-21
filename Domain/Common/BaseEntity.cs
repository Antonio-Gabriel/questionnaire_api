using System.ComponentModel.DataAnnotations.Schema;

namespace QuestionaryApp.Domain.Common
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public void SetId(Guid id)
        {
            Id = id;
        }

        private readonly List<BaseEvent> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<BaseEvent> DomainsEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(BaseEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomain(BaseEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void CleanDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}