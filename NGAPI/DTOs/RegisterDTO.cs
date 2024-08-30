using System.ComponentModel.DataAnnotations;

namespace NGAPI.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [MaxLength(100)]
        public required string UserName { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
