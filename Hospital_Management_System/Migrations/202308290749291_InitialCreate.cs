namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            

            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientName = c.String(nullable: false, maxLength: 50),
                        AdmissionDate = c.DateTime(nullable: false, storeType: "date"),
                        Mobile = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Gender = c.Int(nullable: false),
                        Picture = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Appointments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    DoctorName = c.String(nullable: false, maxLength: 50),
                    AppointmentDate = c.DateTime(nullable: false, storeType: "date"),
                    PatientId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            CreateStoredProcedure("dbo.DeletePatient",
               p => new { id = p.Int() },
               "DELETE FROM dbo.Patients WHERE id=@id");

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "PatientId", "dbo.Patients");
            DropIndex("dbo.Appointments", new[] { "PatientId" });
            DropTable("dbo.Patients");
            DropTable("dbo.Appointments");
            DropStoredProcedure("dbo.DeletePatient");
        }
    }
}
