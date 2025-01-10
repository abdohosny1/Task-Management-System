using System.ComponentModel.DataAnnotations;

namespace MyTask_Management_System.Dto
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
            ErrorMessage = "Password must have 1 UpperCase,1 lower  Case ,1 Number ,1 Non alphanumer and at least 6 character")]
        public string Password { get; set; }
        // [Required]

        public string DisplyName { get; set; }

    }
}
