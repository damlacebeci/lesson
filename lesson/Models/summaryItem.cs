using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lesson.Models
{
    public class summaryItem
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Please enter a title")]
        [MaxLength(200)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a content")]
        public string Content { get; set; }

        [Display(Name ="Did you work?")]
        public bool Worked { get; set; }

        
        public int lessonNameId { get; set; }
        public virtual lessonName lessonName { get; set; }

    }
}
