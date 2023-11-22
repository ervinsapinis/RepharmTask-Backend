namespace RepharmTaskBackend.DTOs
{
    public class PatientListItemDto
    {
        public Guid Id { get; set; }
        public string PersonCode { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
