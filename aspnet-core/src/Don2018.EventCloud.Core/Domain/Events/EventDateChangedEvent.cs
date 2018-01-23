using System;
using System.Collections.Generic;
using System.Text;
using Abp.Events.Bus.Entities;

namespace Don2018.EventCloud.Domain.Events
{
   public class EventDateChangedEvent : EntityEventData<Event>
    {
        public EventDateChangedEvent(Event entity) : base(entity)
        {

        }
    }
}
