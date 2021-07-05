using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lesson.Models
{
    public class lessonUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Job { get; set; }
        public virtual List<summaryItem> SummaryItems { get; set; }
    }
}
