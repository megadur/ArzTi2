using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using ArzTiServer.Api.Models;
using ArzTiServer.Api.Services;
using ArzTiServer.Domain.Repositories;

namespace ArzTiServer.Api.Tests
{
    public class PrescriptionServiceTests
    {
        private readonly Mock<IPrescriptionRepository> _repoMock;
        private readonly PrescriptionService _service;

        public PrescriptionServiceTests()
        {
            _repoMock = new Mock<IPrescriptionRepository>();
            _service = new PrescriptionService(_repoMock.Object);
        }

        [Fact]
        public async Task GetNewPrescriptionsAsync_ReturnsDtos()
        {
            // Arrange
            var repoResult = new List<object> {
                new { Type = "eMuster16", RezeptStatus = "NEU", IdSenderezepteEmuster16 = 1, Muster16Id = "M16-1" },
                new { Type = "P-Rezept", RezeptStatus = "NEU", IdSenderezeptePrezept = 2, TransaktionsNummer = 12345L },
                new { Type = "E-Rezept", RezeptStatus = "NEU", IdSenderezepteErezept = 3, ErezeptId = "E-1" }
            };
            _repoMock.Setup(r => r.GetNewPrescriptionsAsync(1, 10)).ReturnsAsync(repoResult);

            // Act
            var result = await _service.GetNewPrescriptionsAsync(1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                dto => Assert.Equal("eMuster16", dto.Type),
                dto => Assert.Equal("P-Rezept", dto.Type),
                dto => Assert.Equal("E-Rezept", dto.Type)
            );
        }

        [Fact]
        public async Task MarkAsReadAsync_CallsRepository()
        {
            // Arrange
            var dtos = new List<PrescriptionDto> { new PrescriptionDto { Type = "eMuster16" } };
            _repoMock.Setup(r => r.MarkAsReadAsync(It.IsAny<IEnumerable<object>>())).Returns(Task.CompletedTask);

            // Act
            await _service.MarkAsReadAsync(dtos);

            // Assert
            _repoMock.Verify(r => r.MarkAsReadAsync(It.IsAny<IEnumerable<object>>()), Times.Once);
        }

        [Fact]
        public async Task SetStatusAbgerechnetAsync_CallsRepository()
        {
            // Arrange
            var dtos = new List<PrescriptionDto> { new PrescriptionDto { Type = "P-Rezept" } };
            _repoMock.Setup(r => r.SetStatusAbgerechnetAsync(It.IsAny<IEnumerable<object>>())).Returns(Task.CompletedTask);

            // Act
            await _service.SetStatusAbgerechnetAsync(dtos);

            // Assert
            _repoMock.Verify(r => r.SetStatusAbgerechnetAsync(It.IsAny<IEnumerable<object>>()), Times.Once);
        }
    }
}