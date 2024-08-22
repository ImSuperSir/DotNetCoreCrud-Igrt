using System.ComponentModel.DataAnnotations;

namespace TaskApi.Api.Dtos
{
    public class UpdatePermissioinDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; } = string.Empty;
        // public string Role { get; set; } = string.Empty;
        // public string[] Permissions { get; set; } = Array.Empty<string>();
    }
}