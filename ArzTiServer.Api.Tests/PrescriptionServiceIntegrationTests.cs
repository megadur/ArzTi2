using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using ArzTiServer.Api.Models;
using ArzTiServer.Api.Services;
using ArzTiServer.Domain.Model.ApoTi;
using ArzTiServer.Domain.Repositories;

namespace ArzTiServer.Api.Tests
{
    public class PrescriptionServiceIntegrationTests
    {
        private PrescriptionService CreateServiceWithInMemoryDb(out ArzTiDbContext dbContext)
        {
            var options = new DbContextOptionsBuilder<ArzTiDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            dbContext = new ArzTiDbContext(options);
            var repo = new PrescriptionRepository(dbContext);
            return new PrescriptionService(repo);
        }

        [Fact]
        public async Task GetNewPrescriptionsAsync_ReturnsInsertedData()
        {
            // Arrange
            var service = CreateServiceWithInMemoryDb(out var db);
            db.ErSenderezepteEmuster16Statuses.Add(new ErSenderezepteEmuster16Status {
                IdSenderezepteEmuster16 = 1,
                RezeptStatus = "NEU",
                Muster16Id = "M16-1"
            });
            db.ErSenderezeptePrezeptStatuses.Add(new ErSenderezeptePrezeptStatus {
                IdSenderezeptePrezept = 2,
                RezeptStatus = "NEU",
                TransaktionsNummer = 12345L
            });
            db.ErSenderezepteErezeptStatuses.Add(new ErSenderezepteErezeptStatus {
                IdSenderezepteErezept = 3,
                RezeptStatus = "NEU",
                ErezeptId = "E-1"
            });
            await db.SaveChangesAsync();

            // Act
            var result = await service.GetNewPrescriptionsAsync(1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                dto => Assert.Equal("eMuster16", dto.Type),
                dto => Assert.Equal("P-Rezept", dto.Type),
                dto => Assert.Equal("E-Rezept", dto.Type)
            );
        }
    }
}