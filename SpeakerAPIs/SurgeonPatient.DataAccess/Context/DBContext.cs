namespace SurgeonPatient.DataAccess.Context
{
    using System.Data.Entity;
    using SurgeonPatient.DataAccess.Entities;

    public class DBContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
    }
}
