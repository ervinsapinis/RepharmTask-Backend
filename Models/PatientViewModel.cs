using RepharmTaskBackend.Constants;

namespace RepharmTaskBackend.Models
{
    public class PatientViewModel
    {
        public Guid? id { get; set; }
        public string PersonCode { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateBirth { get; set; }
        public string Sex { get; set; }
        public Guid DoctorId { get; set; }

    }
}
