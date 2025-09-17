using System;
using System.Collections.Generic;

namespace ArzTiServer.Domain.Model.ApoTi
{
    public partial class ErSenderezepteErezeptDaten
    {
        public int IdSenderezepteErezeptDaten { get; set; }
        public int? IdSenderezepteErezept { get; set; }
        /// <summary>
        /// Eindeutiger Rezept - Universal Unique Identifier (UUID)
        /// </summary>
        public string RezeptUuid { get; set; } = null!;
        /// <summary>
        /// Bestätigung vom ARZ - Transfer der Rezeptdaten erfolgt
        /// </summary>
        public bool? TransferArz { get; set; }
        /// <summary>
        /// Original XML-Request der Apotheke (nur Rezept-XML) - zur Weiterleitung ins ARZ
        /// </summary>
        public string XmlRequest { get; set; } = null!;

        public virtual ErSenderezepteErezept? IdSenderezepteErezeptNavigation { get; set; }
    }
}
