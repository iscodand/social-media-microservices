using CQRS.Core.Messages;

namespace CQRS.Core.Events
{
    public abstract class BaseEvent : Message
    {
        public BaseEvent(string type)
        {
            this.Type = type;
        }

        public string Type { get; set; }
        public int Version { get; set; }
    }

}