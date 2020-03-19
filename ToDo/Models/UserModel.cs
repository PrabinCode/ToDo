using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo.Models
{
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required] [EmailAddress] public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Secret Word")]
        public string SecretWord { get; set; }

        [Display(Name = "Created Date")] public DateTime? CreatedDate { get; set; }

        [Display(Name = "Modified Date")] public DateTime? ModifiedDate { get; set; }
    }
}