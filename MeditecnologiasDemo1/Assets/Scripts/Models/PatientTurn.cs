namespace Assets.Scripts.Models
{
    using System;

    public class PatientTurn
    {
        public Patient Patient;
        public DateTime TurnDate;
        public string Medic; // TODO: Replace with an entity.
        public string Observation;

        public PatientTurn()
        {
            this.Patient = new Patient();
        }
    }
}
