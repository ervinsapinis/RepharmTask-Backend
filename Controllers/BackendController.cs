using MediatR;
using Microsoft.AspNetCore.Mvc;
using RepharmTaskBackend.Commands;
using RepharmTaskBackend.Models;
using RepharmTaskBackend.Queries;

namespace RepharmTaskBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BackendController : Controller
    {
        private readonly IMediator _mediator;

        public BackendController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("patients/{doctorId?}")]
        public async Task<IActionResult> GetPatients([FromRoute] Guid? doctorId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetPatientsQuery() { DoctorId = doctorId }, cancellationToken);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("doctors")]
        public async Task<IActionResult> GetDoctors([FromRoute] CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetDoctorsQuery(), cancellationToken);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost("patient")]
        public async Task<IActionResult> CreatePatient([FromBody] PatientViewModel model, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new CreatePatientCommand { Model = model}, cancellationToken);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
