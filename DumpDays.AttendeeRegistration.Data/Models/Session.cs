using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DumpDays.AttendeeRegistration.Data.Models
{
    public class Session
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid         Id        { get; set; }
                                      
        [Required]                    
        public DateTime     StartedOn { get; set; }
                                      
        [Required]                    
        public virtual User User      { get; set; }
    }
}
