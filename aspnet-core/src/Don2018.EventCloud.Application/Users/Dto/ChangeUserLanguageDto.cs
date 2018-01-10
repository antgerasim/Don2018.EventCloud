using System.ComponentModel.DataAnnotations;

namespace Don2018.EventCloud.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}