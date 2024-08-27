namespace EventBus.Messages.IntegrationEvents
{
    public record IntegrationBaseEvent() : IIntegrationEvent
    {
        public DateTime CreationDate { get; } = DateTime.Now;
        public Guid Id { get; set; }
    }
}