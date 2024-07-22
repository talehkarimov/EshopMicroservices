using MediatR;

namespace Ordering.Domain.Abstractions
{
    public interface IDomainEvent : INotification
    {
        Guid EventId => Guid.NewGuid();
        public DateTime OccureOn => DateTime.Now;
        public string EventType => GetType().AssemblyQualifiedName;
    }
}
