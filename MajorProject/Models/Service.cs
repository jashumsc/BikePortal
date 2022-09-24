using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajorProject.Models
{
    [Table("Services")]
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ServiceId { get; set; }

        [Required]
        [StringLength(50)]  
        public string CustomerName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                  ErrorMessage = "Entered phone format is not valid.")]

        public string CustomerPhone { get; set; }


        [Required]
        public string BikeNumber { get; set; }


        [Required]
        public string ServiceOn { get; set; }


        [Display(Name = "Service Status")]
        public bool Status { get; set; }
    }
}
