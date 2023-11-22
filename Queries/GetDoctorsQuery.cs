using RepharmTaskBackend.DTOs;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using System.Net;

namespace RepharmTaskBackend.Queries
{
    public record GetDoctorsQuery : IRequest<BaseResponse<List<DoctorListItemDto>>>
    {
    }

    public class GetDoctorsQueryHandler : IRequestHandler<GetDoctorsQuery, BaseResponse<List<DoctorListItemDto>>>
    {
        private readonly BackendContext _context;
        private readonly IMapper _mapper;

        public GetDoctorsQueryHandler(BackendContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BaseResponse<List<DoctorListItemDto>>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Doctors.AsNoTracking();

            var result = await query
                .OrderBy(d => d.Surname)
                .ProjectTo<DoctorListItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new BaseResponse<List<DoctorListItemDto>>(result, false, null, HttpStatusCode.OK);
        }
    }
}
