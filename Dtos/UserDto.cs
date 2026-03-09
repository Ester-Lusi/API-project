using System.ComponentModel.DataAnnotations;

namespace Dtos
{
    public record UserDto
    (
        int Id,
        string FirstName,
        string LastName,
        [Required]
        [EmailAddress]
        string Email
    );
}
