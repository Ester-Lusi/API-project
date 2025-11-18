using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class User
    {
        public int Id { get; set; }
        [EmailAddress(ErrorMessage ="Enter A real Email:)"), Required]
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [StringLength(12, ErrorMessage = "password must be between 4 - 12 chars", MinimumLength = 4), Required]
        public string Password { get; set; }

    }
}
