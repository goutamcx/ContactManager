using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactManager.Models
{
    public class Contact
    {
        public int ID { get; set; }
        [StringLength(60, MinimumLength = 2)]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [StringLength(60, MinimumLength = 2)]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$")]
        [Display(Name = "e-mail")]
        public string Email { get; set; }        
        [Required]
        [StringLength(12, MinimumLength = 3)]
        public string Phone { get; set; }
        public bool IsActive { get; set; }
    }
}
