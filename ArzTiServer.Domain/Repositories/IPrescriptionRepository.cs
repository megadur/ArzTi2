using System.Collections.Generic;
using System.Threading.Tasks;
namespace ArzTiServer.Domain.Repositories
{
    public interface IPrescriptionRepository
    {
        Task<IEnumerable<object>> GetNewPrescriptionsAsync(int page, int pageSize);
        Task TransferPrescriptionsAsync(IEnumerable<object> prescriptions);
        Task MarkAsReadAsync(IEnumerable<object> prescriptions);
        Task SetStatusAbgerechnetAsync(IEnumerable<object> prescriptions);
    }
}