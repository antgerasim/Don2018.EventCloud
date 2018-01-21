using System;

namespace Don2018.EventCloud.Domain.Events.Dtos
{
    public class CreateEventInput
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int NaxRegistrationCount { get; set; }
    }
}