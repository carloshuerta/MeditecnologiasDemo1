using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Assets.Scripts.Models;

namespace Assets.Scripts.Scheduler
{
    public static class PatientsRepository
    {
        private static readonly IList<Patient> patients = new List<Patient>
        {
            new Patient
            {
                PatientId = 1,
                Name = "Sebastian Gambolati",
                BirthDate = DateTime.ParseExact("20/04/1980", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                DocumentType = "DNI",
                DocumentNumber = 28080291,
                InternalID = 635063
            },
            new Patient
            {
                PatientId = 2,
                Name = "Gustavo Bugna",
                BirthDate = DateTime.ParseExact("02/07/1980", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                DocumentType = "DNI",
                DocumentNumber = 28563447,
                InternalID = 78157
            },
            new Patient
            {
                PatientId = 3,
                Name = "Carlos Huerta",
                BirthDate = DateTime.ParseExact("18/11/1981", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                DocumentType = "DNI",
                DocumentNumber = 27153263,
                InternalID = 125198
            },
            new Patient
            {
                PatientId = 4,
                Name = "Saira Ruiz",
                BirthDate = DateTime.ParseExact("01/02/1978", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                DocumentType = "DNI",
                DocumentNumber = 26477626,
                InternalID = 6779310
            },
            new Patient
            {
                PatientId = 5,
                Name = "Lidia Bermudez",
                BirthDate = DateTime.ParseExact("13/11/1956", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                DocumentType = "DNI",
                DocumentNumber = 3645893,
                InternalID = 232491
            },
            new Patient
            {
                PatientId = 6,
                Name = "Hugo Alvornoz",
                BirthDate = DateTime.ParseExact("12/07/1966", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                DocumentType = "DNI",
                DocumentNumber = 13431849,
                InternalID = 984038
            }
        };

        public static Patient GetPatient(int patientId)
        {
            return patients.First(x => x.PatientId == patientId);
        }
    }
}
