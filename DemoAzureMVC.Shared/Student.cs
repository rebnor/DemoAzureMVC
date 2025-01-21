using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoAzureMVC.Shared
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string? PictureUrl { get; set; }

        // If true, is male/wizard. If false, is female/witch
        public bool IsWizard { get; set; }
    }
}
