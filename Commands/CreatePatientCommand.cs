using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RepharmTaskBackend.Constants;
using RepharmTaskBackend.DTOs;
using RepharmTaskBackend.Entities;
using RepharmTaskBackend.Models;
using System.Net;

namespace RepharmTaskBackend.Commands
{
        public record CreatePatientCommand : IRequest<BaseResponse<PatientDto>>
        {
            public PatientViewModel Model { get; set; }
        }

        public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, BaseResponse<PatientDto>>
        {

            private readonly IMapper _mapper;
            private readonly BackendContext _context;

            public CreatePatientCommandHandler(BackendContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<BaseResponse<PatientDto>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
            {
                var doctorExists = await _context.Doctors.AnyAsync(d => d.Id == request.Model.DoctorId);

                if(!doctorExists)
                    return new BaseResponse<PatientDto>(null, true, ErrorCodes.InvalidArgument, HttpStatusCode.BadRequest);

                var patientToAdd = _mapper.Map<Patient>(request.Model);
                patientToAdd.DateCreated = DateTime.UtcNow;
                patientToAdd.DateUpdated = DateTime.UtcNow;

                await _context.AddAsync(patientToAdd);
                await _context.SaveChangesAsync(cancellationToken);

                var result = _mapper.Map<PatientDto>(patientToAdd);

                return new BaseResponse<PatientDto>(result, true, null, HttpStatusCode.OK);
            }
        }
}
