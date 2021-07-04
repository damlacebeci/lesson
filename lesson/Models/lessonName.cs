using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lesson.Models
{
    public class lessonName
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a lesson name")]
        [MaxLength(50)]
        public string Name { get; set; }
        public virtual List<summaryItem> summaryItems { get; set; }
    }
}
