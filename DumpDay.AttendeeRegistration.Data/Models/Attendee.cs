using System;
using System.ComponentModel.DataAnnotations;

namespace DumpDay.AttendeeRegistration.Data.Models
{
    public enum WorkStatuses
    {
        Pupil,
        Student,
        Employed,
        Unemployed,
        Retired
    }

    public class Attendee
    {
        [Key]
        public int          Id          { get; set; }
                                        
        [Required]                      
        public string       FirstName   { get; set; }
                                        
        [Required]                      
        public string       LastName    { get; set; }
                                        
        [Required]                      
        public DateTime     Birthdate   { get; set; }
                                        
        [Required]                      
        public WorkStatuses WorkStatus  { get; set; }
                                        
        public string       Institution { get; set; }
                                        
        [Required]                      
        public DateTime     CreatedOn   { get; set; }
    }
}
