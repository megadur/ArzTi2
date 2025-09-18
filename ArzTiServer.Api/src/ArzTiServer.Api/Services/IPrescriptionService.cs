using System.Collections.Generic;
using System.Threading.Tasks;
using ArzTiServer.Api.Models;

namespace ArzTiServer.Api.Services
{
    public interface IPrescriptionService
    {
        Task<IEnumerable<PrescriptionDto>> GetNewPrescriptionsAsync(int page, int pageSize);
        Task TransferPrescriptionsAsync(IEnumerable<PrescriptionDto> prescriptions);
        Task MarkAsReadAsync(IEnumerable<PrescriptionDto> prescriptions);
        Task SetStatusAbgerechnetAsync(IEnumerable<PrescriptionDto> prescriptions);
    }
}