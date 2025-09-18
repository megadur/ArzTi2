using System.Collections.Generic;
using System.Threading.Tasks;
using ArzTiServer.Api.Models;

namespace ArzTiServer.Api.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        public Task<IEnumerable<PrescriptionDto>> GetNewPrescriptionsAsync(int page, int pageSize)
        {
            throw new System.NotImplementedException();
        }

        public Task TransferPrescriptionsAsync(IEnumerable<PrescriptionDto> prescriptions)
        {
            throw new System.NotImplementedException();
        }

        public Task MarkAsReadAsync(IEnumerable<PrescriptionDto> prescriptions)
        {
            throw new System.NotImplementedException();
        }

        public Task SetStatusAbgerechnetAsync(IEnumerable<PrescriptionDto> prescriptions)
        {
            throw new System.NotImplementedException();
        }
    }
}