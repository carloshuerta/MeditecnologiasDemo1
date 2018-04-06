using System.Web.Http;
using System.Linq;
using SurgeonPatient.DataAccess.Context;
using SurgeonPatient.DataAccess.Entities;
using System.Threading.Tasks;
using SurgeonPatient.DataAccess.Repositories;
using System.Collections.Generic;

namespace SpeakerAPI.Controllers
{
    public class PatientController : ApiController
    {
        private readonly IPatientRepository patientRepository;

        public PatientController()
            : this(new PatientRepository())
        {
        }

        public PatientController(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        public List<Patient> Get()
        {
            return this.patientRepository.List();
        }

        public async Task<Patient> Get(int id)
        {
            return await this.patientRepository.GetAsync(id);
        }

        [HttpPost]
        public async Task<int> Post(Patient patient)
        {
            return await this.patientRepository.InsertAsync(patient);
        }
    }
}
