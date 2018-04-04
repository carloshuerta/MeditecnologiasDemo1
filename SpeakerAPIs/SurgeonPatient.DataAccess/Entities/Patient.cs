using System;

namespace SurgeonPatient.DataAccess.Entities
{
    public class Patient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
