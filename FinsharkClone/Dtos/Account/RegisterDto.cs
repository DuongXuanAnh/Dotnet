using System.ComponentModel.DataAnnotations;

namespace FinsharkClone.Dtos.Account
{
    //  dotnet ef migrations add SeedRole   
    // dotnet ef database update  
    public class RegisterDto
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}