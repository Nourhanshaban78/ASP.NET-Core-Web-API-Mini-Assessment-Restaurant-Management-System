using System.ComponentModel.DataAnnotations;

namespace UserIdentityProject.Models
{
    public class TokenRequestModel
    {
        [MaxLength(128)]
        public string Email { get; set; }
        [MaxLength(256)]
        public string Password { get; set; }
    }
}
