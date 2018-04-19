
namespace Assets.Scripts.General
{
    using MixedRealityToolkit.Common;

    public class SessionData : Singleton<SessionData>
    {
        public string LoggedMedicId { get; set; }

        public string LoggedMedicName { get; set; }

        public int ViewingPatientId { get; set; }

        public string ViewingPatientName { get; set; }

        public bool HasLogged
        {
            get
            {
                return !string.IsNullOrEmpty(this.LoggedMedicId) && !string.IsNullOrEmpty(this.LoggedMedicName);
            }
        }
    }
}
