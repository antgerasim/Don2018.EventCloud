using System;
using System.ComponentModel.DataAnnotations;

namespace Don2018.EventCloud.Domain.Events.Dtos
{

    public class CreateEventInput
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int MaxRegistrationCount { get; set; }
    }
}