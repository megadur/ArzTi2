using System;
using System.Collections.Generic;

namespace ArzTiServer.Domain.Model.ArzSw
{
    public partial class ErezeptFhirProfile
    {
        public int ErezeptFhirProfileId { get; set; }
        /// <summary>
        /// E-Rezept FHIR Profile Version
        /// </summary>
        public string ErezeptFhirProfileVersion { get; set; } = null!;
        public string ErezeptFhirProfileUrl { get; set; } = null!;
        public string QuellKenner { get; set; } = null!;
        public string? Beschreibung { get; set; }
        public DateOnly GueltigVon { get; set; }
        public DateOnly GueltigBis { get; set; }
    }
}
