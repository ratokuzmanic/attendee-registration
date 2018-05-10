using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DumpDays.AttendeeRegistration.Common;

namespace DumpDays.AttendeeRegistration.Data.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid   Id       { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Roles  Role     { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool   IsActive { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool   IsSetup  { get; set; }
    }
}
