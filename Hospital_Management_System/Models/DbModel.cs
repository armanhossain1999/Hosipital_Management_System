using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hospital_Management_System.Models
{
    public enum Gender { Male = 1, Female }
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
    public class Patient:EntityBase
    {
     
        [Required, StringLength(50)]
        public string PatientName { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime AdmissionDate { get; set; }
        [Required, Column(TypeName = "varchar")]
        public string Mobile { get; set; }
        [Required, EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        [Required, StringLength(50)]
        public string Picture { get; set; }

        //nav
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
    public class Appointment:EntityBase
    {
        
        [Required, StringLength(50)]
        public string DoctorName { get; set; }

        [Required, Column(TypeName = "date")]
        public DateTime AppointmentDate { get; set; }

        [Required, ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

    }
   public class PatientDbContext : DbContext
    {
        public PatientDbContext()
        {
            Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }


}