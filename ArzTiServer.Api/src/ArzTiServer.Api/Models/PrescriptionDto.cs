using System;

namespace ArzTiServer.Api.Models
{
    public class PrescriptionDto
    {
        public Guid Uuid { get; set; }
        public string BusinessKey { get; set; }
        public string Type { get; set; }
        public bool TransferArz { get; set; }
        public string RezeptStatus { get; set; }
        // Add other relevant fields as needed
    }
}