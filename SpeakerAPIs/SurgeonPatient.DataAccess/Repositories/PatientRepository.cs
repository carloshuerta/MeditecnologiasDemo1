namespace SurgeonPatient.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using SurgeonPatient.DataAccess.Context;
    using SurgeonPatient.DataAccess.Entities;

    public class PatientRepository : IPatientRepository
    {
        private readonly DBContext dbContext;

        public PatientRepository()
        {
            this.dbContext = new DBContext();
        }

        public async Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Patient> GetAsync(int id)
        {
            return await this.dbContext.Patients.FindAsync(id);
        }

        public async Task<int> InsertAsync(Patient patient)
        {
            this.dbContext.Patients.Add(patient);
            return await this.dbContext.SaveChangesAsync();
        }

        public List<Patient> List()
        {
            return this.dbContext.Patients.ToList();
        }

        public async Task<int> UpdateAsync(Patient patient)
        {
            throw new NotImplementedException();
        }
    }
}
