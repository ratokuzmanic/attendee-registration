using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DumpDays.AttendeeRegistration.Common;

namespace DumpDays.AttendeeRegistration.Data.Models
{
    public class Attendee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid         Id                      { get; set; }
                                                    
        [Required]                                  
        public string       FirstName               { get; set; }
                                                    
        [Required]                                  
        public string       LastName                { get; set; }

        [Required]
        public string       Email                   { get; set; }

        [Required]                                  
        public DateTime     Birthdate               { get; set; }
                                                    
        [Required]                                  
        public WorkStatuses WorkStatus              { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool         IsAccreditationPrinted  { get; set; }

        [Required]                      
        public DateTime     CreatedOn               { get; set; }
    }
}
