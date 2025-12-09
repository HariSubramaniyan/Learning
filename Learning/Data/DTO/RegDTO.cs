using System.ComponentModel.DataAnnotations;

namespace Learning.Data.DTO
{
    public class RegDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}
