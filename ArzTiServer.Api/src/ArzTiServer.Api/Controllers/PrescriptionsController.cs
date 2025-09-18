using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArzTiServer.Api.Models;
using ArzTiServer.Api.Services;

namespace ArzTiServer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionsController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet("new")]
        public async Task<IEnumerable<PrescriptionDto>> GetNewPrescriptions(int page = 1, int pageSize = 100)
        {
            return await _prescriptionService.GetNewPrescriptionsAsync(page, pageSize);
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> TransferPrescriptions([FromBody] IEnumerable<PrescriptionDto> prescriptions)
        {
            await _prescriptionService.TransferPrescriptionsAsync(prescriptions);
            return Ok();
        }

        [HttpPost("mark-read")]
        public async Task<IActionResult> MarkAsRead([FromBody] IEnumerable<PrescriptionDto> prescriptions)
        {
            await _prescriptionService.MarkAsReadAsync(prescriptions);
            return Ok();
        }

        [HttpPost("set-status")]
        public async Task<IActionResult> SetStatusAbgerechnet([FromBody] IEnumerable<PrescriptionDto> prescriptions)
        {
            await _prescriptionService.SetStatusAbgerechnetAsync(prescriptions);
            return Ok();
        }
    }
}