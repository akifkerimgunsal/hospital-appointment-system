
namespace Business.Exceptions
{
    public class NotFoundException : Exception
    {
        public string? EntityName { get; set; }
        public object? EntityKey { get; set; }
        public Guid EntityId { get; set; }

        public NotFoundException(string entityName, Guid entityId)
            : base($"Entity '{entityName}' with ID '{entityId}' was not found.")
        {
            EntityName = entityName;
            EntityId = entityId;
        }

        public NotFoundException(string entityName, string entityKey)
            : base($"Entity '{entityName}' with key '{entityKey}' was not found.")
        {
            EntityName = entityName;
            EntityKey = entityKey;
        }

        public NotFoundException(string message) : base(message)
        {
        }
    }
}
