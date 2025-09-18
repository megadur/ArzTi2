using System;
using System.Collections.Generic;
using System.Linq;
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
            try
            {
                var results = await _repository.GetNewPrescriptionsAsync(page, pageSize);
                var dtos = results.Select(x => {
                    try
                    {
                        dynamic d = x;
                        var dto = new Models.PrescriptionDto {
                            Type = d.Type,
                            RezeptStatus = d.RezeptStatus,
                            Uuid = Guid.Empty, // Default, will set below
                            BusinessKey = ""
                        };
                        if (d.Type == "eMuster16")
                        {
                            dto.Uuid = d.IdSenderezepteEmuster16 != null ? Guid.Parse(d.IdSenderezepteEmuster16.ToString()) : Guid.Empty;
                            dto.BusinessKey = d.Muster16Id != null ? d.Muster16Id.ToString() : "";
                        }
                        else if (d.Type == "P-Rezept")
                        {
                            dto.Uuid = d.IdSenderezeptePrezept != null ? Guid.Parse(d.IdSenderezeptePrezept.ToString()) : Guid.Empty;
                            dto.BusinessKey = d.TransaktionsNummer != null ? d.TransaktionsNummer.ToString() : "";
                        }
                        else if (d.Type == "E-Rezept")
                        {
                            dto.Uuid = d.IdSenderezepteErezept != null ? Guid.Parse(d.IdSenderezepteErezept.ToString()) : Guid.Empty;
                            dto.BusinessKey = d.ErezeptId != null ? d.ErezeptId.ToString() : "";
                        }
                        return dto;
                    }
                    catch (Exception ex)
                    {
                        // Log or handle mapping error
                        return null;
                    }
                })
                .Where(dto => dto != null)
                .ToList();
                return dtos;
            }
            catch (Exception ex)
            {
                // Log or handle repository error
                throw new ApplicationException("Failed to get new prescriptions.", ex);
            }
        }

        public async Task TransferPrescriptionsAsync(IEnumerable<Models.PrescriptionDto> prescriptions)
        {
            try
            {
                await _repository.TransferPrescriptionsAsync(prescriptions);
            }
            catch (Exception ex)
            {
                // Log or handle error
                throw new ApplicationException("Failed to transfer prescriptions.", ex);
            }
        }

        public async Task MarkAsReadAsync(IEnumerable<Models.PrescriptionDto> prescriptions)
        {
            try
            {
                await _repository.MarkAsReadAsync(prescriptions);
            }
            catch (Exception ex)
            {
                // Log or handle error
                throw new ApplicationException("Failed to mark prescriptions as read.", ex);
            }
        }

        public async Task SetStatusAbgerechnetAsync(IEnumerable<Models.PrescriptionDto> prescriptions)
        {
            try
            {
                await _repository.SetStatusAbgerechnetAsync(prescriptions);
            }
            catch (Exception ex)
            {
                // Log or handle error
                throw new ApplicationException("Failed to set prescriptions as ABGERECHNET.", ex);
            }
        }
    }
}