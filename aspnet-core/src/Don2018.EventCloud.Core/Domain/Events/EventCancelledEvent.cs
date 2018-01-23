using Abp.Events.Bus.Entities;

namespace Don2018.EventCloud.Domain.Events
{
    public class EventCancelledEvent : EntityEventData<Event>
    {
        public EventCancelledEvent(Event entity) : base(entity)
        {
        }
    }
}