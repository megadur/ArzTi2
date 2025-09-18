using System.Collections.Generic;
using System.Threading.Tasks;
using ArzTiServer.Api.Models;

namespace ArzTiServer.Api.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly Domain.Repositories.IPrescriptionRepository _repository;
        public PrescriptionService(Domain.Repositories.IPrescriptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Models.PrescriptionDto>> GetNewPrescriptionsAsync(int page, int pageSize)
        {
            var results = await _repository.GetNewPrescriptionsAsync(page, pageSize);
            // Map anonymous objects to PrescriptionDto
            var dtos = results.Select(x => {
                dynamic d = x;
                return new Models.PrescriptionDto {
                    Type = d.Type,
                    RezeptStatus = d.RezeptStatus,
                    // Map IDs to Uuid/BusinessKey as appropriate (for demo, just use IDs)
                    Uuid = d.GetType().GetProperty("IdSenderezepteEmuster16") != null ? Guid.Empty :
                           d.GetType().GetProperty("IdSenderezeptePrezept") != null ? Guid.Empty :
                           d.GetType().GetProperty("IdSenderezepteErezept") != null ? Guid.Empty : Guid.Empty,
                    BusinessKey = "",
                };
            }).ToList();
            return dtos;
        }

        public async Task TransferPrescriptionsAsync(IEnumerable<Models.PrescriptionDto> prescriptions)
        {
            // Pass through to repository (convert to object for now)
            await _repository.TransferPrescriptionsAsync(prescriptions);
        }

        public async Task MarkAsReadAsync(IEnumerable<Models.PrescriptionDto> prescriptions)
        {
            await _repository.MarkAsReadAsync(prescriptions);
        }

        public async Task SetStatusAbgerechnetAsync(IEnumerable<Models.PrescriptionDto> prescriptions)
        {
            await _repository.SetStatusAbgerechnetAsync(prescriptions);
        }
    }
}