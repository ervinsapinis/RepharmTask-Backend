using RepharmTaskBackend.DTOs;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using System.Net;

namespace RepharmTaskBackend.Queries
{
    public record GetPatientsQuery : IRequest<BaseResponse<List<PatientListItemDto>>>
    {
        public Guid? DoctorId { get; init; }
    }

    public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, BaseResponse<List<PatientListItemDto>>>
    {
        private readonly BackendContext _context;
        private readonly IMapper _mapper;

        public GetPatientsQueryHandler(BackendContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BaseResponse<List<PatientListItemDto>>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Patients.AsNoTracking();

            if (request.DoctorId.HasValue)
                query = query.Where(p => p.DoctorId == request.DoctorId);

            var result = await query
                .OrderBy(p => p.Surname)
                .ProjectTo<PatientListItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new BaseResponse<List<PatientListItemDto>>(result, false, null, HttpStatusCode.OK);
        }
    }
}
