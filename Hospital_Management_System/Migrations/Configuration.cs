namespace Hospital_Management_System.Migrations
{
    using Hospital_Management_System.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Hospital_Management_System.Models.PatientDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Hospital_Management_System.Models.PatientDbContext context)
        {
            Patient p1 = new Patient { PatientName = "Abdulla", AdmissionDate = DateTime.Parse("2023-02-02"), Mobile = "01309059311", Gender = Gender.Male, Picture = "1.jpg", };
            p1.Appointments.Add(new Appointment { DoctorName = "Nishat Rahman", AppointmentDate = DateTime.Parse("2023-7-2"), PatientId = 1 });
            p1.Appointments.Add(new Appointment { DoctorName = "Matiur Rahman", AppointmentDate = DateTime.Parse("2023-7-8"), PatientId = 1 });
            context.Patients.Add(p1);
            context.SaveChanges();
        }
    }
}
