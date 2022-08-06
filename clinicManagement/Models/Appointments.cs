namespace clinicManagement.Models
{
    public class Appointments
    {
        public int id { get; set; }
        public string date { get; set; }
        public string client_name { get; set; }
        public string appointment_type { get; set; }
        public float duration { get; set; }
        public float revenue { get; set; }
        public float cost { get; set; }
        public int practitioner_id { get; set; }

        public Practitioners Practitioners { get; set; }
    }
}
