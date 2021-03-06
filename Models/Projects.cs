using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace designhubAPI.Models
{
    public class Projects
    {
        [Key]
        public int ProjectsID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime DateCreated { get; set; }


        public virtual ICollection <ProjectFileGroup> ProjectFileGroup { get; set; }
    }
}