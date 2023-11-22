using AutoMapper;
using RepharmTaskBackend.Entities;
using RepharmTaskBackend.DTOs;
using RepharmTaskBackend.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Patient, PatientDto>();
        CreateMap<Patient, PatientListItemDto>();
        CreateMap<Doctor, DoctorDto>();
        CreateMap<PatientViewModel, Patient>();
        CreateMap<Doctor, DoctorListItemDto>();
    }
}
