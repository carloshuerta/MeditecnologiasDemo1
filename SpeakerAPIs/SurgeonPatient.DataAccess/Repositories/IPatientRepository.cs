namespace SurgeonPatient.DataAccess.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SurgeonPatient.DataAccess.Entities;

    public interface IPatientRepository
    {
        List<Patient> List();

        Task<Patient> GetAsync(int id);

        Task<int> InsertAsync(Patient patient);

        Task<int> UpdateAsync(Patient patient);

        Task<int> DeleteAsync(int id);
    }
}
