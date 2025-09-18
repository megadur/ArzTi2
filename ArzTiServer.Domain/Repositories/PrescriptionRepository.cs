using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArzTiServer.Domain.Model.ApoTi;
using Microsoft.EntityFrameworkCore;

namespace ArzTiServer.Domain.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly ArzTiDbContext _context;
        public PrescriptionRepository(ArzTiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetNewPrescriptionsAsync(int page, int pageSize)
        {
            // Fetch new eMuster16
            var emuster16 = _context.ErSenderezepteEmuster16Statuses
                .Where(x => x.RezeptStatus != "ABGERECHNET")
                .Select(x => new { Type = "eMuster16", x.IdSenderezepteEmuster16, x.RezeptStatus })
                .Skip((page - 1) * pageSize).Take(pageSize);

            // Fetch new P-Rezept
            var prezept = _context.ErSenderezeptePrezeptStatuses
                .Where(x => x.RezeptStatus != "ABGERECHNET")
                .Select(x => new { Type = "P-Rezept", x.IdSenderezeptePrezept, x.RezeptStatus })
                .Skip((page - 1) * pageSize).Take(pageSize);

            // Fetch new E-Rezept
            var erezept = _context.ErSenderezepteErezeptStatuses
                .Where(x => x.RezeptStatus != "ABGERECHNET")
                .Select(x => new { Type = "E-Rezept", x.IdSenderezepteErezept, x.RezeptStatus })
                .Skip((page - 1) * pageSize).Take(pageSize);

            // Combine all
            var result = emuster16.Cast<object>().Concat(prezept).Concat(erezept);
            // For demo: return as a list (in real code, use async and proper DTOs)
            return await Task.FromResult(result.ToList());
        }

        public async Task TransferPrescriptionsAsync(IEnumerable<object> prescriptions)
        {
            // This method would contain logic to transfer prescription data for copayment mediation.
            // For now, assume this is a no-op or handled elsewhere.
            await Task.CompletedTask;
        }

        public async Task MarkAsReadAsync(IEnumerable<object> prescriptions)
        {
            // Use raw SQL for bulk update
            var emuster16Ids = new List<int>();
            var prezeptIds = new List<int>();
            var erezeptIds = new List<int>();
            foreach (dynamic p in prescriptions)
            {
                string type = p.Type;
                if (type == "eMuster16")
                    emuster16Ids.Add((int)p.IdSenderezepteEmuster16);
                else if (type == "P-Rezept")
                    prezeptIds.Add((int)p.IdSenderezeptePrezept);
                else if (type == "E-Rezept")
                    erezeptIds.Add((int)p.IdSenderezepteErezept);
            }
            if (emuster16Ids.Count > 0)
            {
                var idList = string.Join(",", emuster16Ids);
                await _context.Database.ExecuteSqlRawAsync($"UPDATE ErSenderezepteEmuster16Status SET RezeptStatus = 'GELESEN' WHERE IdSenderezepteEmuster16 IN ({idList})");
            }
            if (prezeptIds.Count > 0)
            {
                var idList = string.Join(",", prezeptIds);
                await _context.Database.ExecuteSqlRawAsync($"UPDATE ErSenderezeptePrezeptStatus SET RezeptStatus = 'GELESEN' WHERE IdSenderezeptePrezept IN ({idList})");
            }
            if (erezeptIds.Count > 0)
            {
                var idList = string.Join(",", erezeptIds);
                await _context.Database.ExecuteSqlRawAsync($"UPDATE ErSenderezepteErezeptStatus SET RezeptStatus = 'GELESEN' WHERE IdSenderezepteErezept IN ({idList})");
            }
        }

        public async Task SetStatusAbgerechnetAsync(IEnumerable<object> prescriptions)
        {
            // Use raw SQL for bulk update
            var emuster16Ids = new List<int>();
            var prezeptIds = new List<int>();
            var erezeptIds = new List<int>();
            foreach (dynamic p in prescriptions)
            {
                string type = p.Type;
                if (type == "eMuster16")
                    emuster16Ids.Add((int)p.IdSenderezepteEmuster16);
                else if (type == "P-Rezept")
                    prezeptIds.Add((int)p.IdSenderezeptePrezept);
                else if (type == "E-Rezept")
                    erezeptIds.Add((int)p.IdSenderezepteErezept);
            }
            if (emuster16Ids.Count > 0)
            {
                var idList = string.Join(",", emuster16Ids);
                await _context.Database.ExecuteSqlRawAsync($"UPDATE ErSenderezepteEmuster16Status SET RezeptStatus = 'ABGERECHNET' WHERE IdSenderezepteEmuster16 IN ({idList})");
            }
            if (prezeptIds.Count > 0)
            {
                var idList = string.Join(",", prezeptIds);
                await _context.Database.ExecuteSqlRawAsync($"UPDATE ErSenderezeptePrezeptStatus SET RezeptStatus = 'ABGERECHNET' WHERE IdSenderezeptePrezept IN ({idList})");
            }
            if (erezeptIds.Count > 0)
            {
                var idList = string.Join(",", erezeptIds);
                await _context.Database.ExecuteSqlRawAsync($"UPDATE ErSenderezepteErezeptStatus SET RezeptStatus = 'ABGERECHNET' WHERE IdSenderezepteErezept IN ({idList})");
            }
        }
    }
}