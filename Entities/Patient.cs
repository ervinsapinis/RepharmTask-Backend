namespace RepharmTaskBackend.Entities
{
    public class Patient
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string PersonCode { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateBirth { get; set; }
        public string Sex { get; set; }
        public Guid DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
