using Hospital_Management_System.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital_Management_System.ViewModels
{
    public class PatientEditModel
    {
        public int Id { get; set; } 
        [Required, StringLength(50)]
        public string PatientName { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime AdmissionDate { get; set; }
        [Required, Column(TypeName = "varchar")]
        public string Mobile { get; set; }
        [Required, EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        public HttpPostedFileBase Picture { get; set; }

        //nav
        public virtual List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}