using Microsoft.EntityFrameworkCore;
using RepharmTaskBackend.Constants;
using RepharmTaskBackend.Entities;

namespace RepharmTaskBackend
{
    public class BackendContext : DbContext
    {
        public BackendContext(DbContextOptions<BackendContext> options) : base(options) { }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Static GUIDs for doctors
            var doctorId1 = Guid.Parse("12345678-1234-1234-1234-123456789012");
            var doctorId2 = Guid.Parse("23456789-2345-2345-2345-234567890123");
            var doctorId3 = Guid.Parse("34567890-3456-3456-3456-345678901234");

            // Seeding Doctors
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id = doctorId1,
                    DateCreated = DateTime.Now,
                    PersonCode = "111111-11111",
                    Name = "John",
                    Surname = "Doe",
                    DateBirth = new DateTime(1970, 1, 1),
                    Sex = Sex.Male
                },
                new Doctor
                {
                    Id = doctorId2,
                    DateCreated = DateTime.Now,
                    PersonCode = "222222-22222",
                    Name = "Emma",
                    Surname = "Smith",
                    DateBirth = new DateTime(1980, 2, 2),
                    Sex = Sex.Male
                },
                new Doctor
                {
                    Id = doctorId3,
                    DateCreated = DateTime.Now,
                    PersonCode = "333333-33333",
                    Name = "James",
                    Surname = "Johnson",
                    DateBirth = new DateTime(1990, 3, 3),
                    Sex = Sex.Male
                }
            );

            // Seeding Patients
            for (int i = 1; i <= 10; i++)
            {
                modelBuilder.Entity<Patient>().HasData(
                    new Patient
                    {
                        Id = Guid.NewGuid(),
                        DateCreated = DateTime.Now,
                        PersonCode = $"{100000 + i}-0000{i}",
                        Name = $"Patient{i}",
                        Surname = $"Surname{i}",
                        DateBirth = new DateTime(2000 + i, i, i),
                        Sex = i % 2 == 0 ? Sex.Male : Sex.Female,
                        DoctorId = i % 3 == 0 ? doctorId1 : (i % 3 == 1 ? doctorId2 : doctorId3)
                    }
                );
            }
        }
    }
}
