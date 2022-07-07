using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace VFKLCore.Functions.Models.VFKLInvitation
{
    /// <summary>
    /// Gruppe invitasjon
    /// </summary>
    public class GruppeInvitasjon
    {
        /// <summary>VurderingsType</summary>
        public string VurderingsType { get; set; }

        /// <summary>BrukerID</summary>
        public string BrukerID { get; set; }

        /// <summary>Læremiddel</summary>
        public string Læremiddel { get; set; }

        /// <summary>LæremiddelLeverandør</summary>
        public string LæremiddelLeverandør { get; set; }

        /// <summary>Læreplan</summary>
        public string Læreplan { get; set; }

        /// <summary>Skolenivå</summary>
        public string Skolenivå { get; set; }

        /// <summary>Utdanningsprogram</summary>
        public string Utdanningsprogram { get; set; }

        /// <summary>Programområde</summary>
        public string Programområde { get; set; }

        /// <summary>GruppeVurderingsID</summary>
        [XmlElement("gruppeVurderingsID")]
        public string GruppeVurderingsID { get; set; }

        /// <summary>VurderingsFrist</summary>
        public string VurderingsFrist { get; set; }

        /// <summary>MottakerEposter</summary>
        public string MottakerEposter { get; set; }
        
        /// <summary>Navn til bruker/// </summary>
        public string Navn { get; set; }
        
        /// <summary>Læreplan kode</summary>
        public string LæreplanKode { get; set; }

        /// <summary>BortvalgteSpørsmål</summary>
        public string BortvalgteSpørsmål { get; set; }

        /// <summary>BortvalgteSpørsmålDel1</summary>
        public string BortvalgteSpørsmålDel1 { get; set; }

        /// <summary>BortvalgteSpørsmålDel2</summary>
        public string BortvalgteSpørsmålDel2 { get; set; }

        /// <summary>BortvalgteSpørsmålDel3</summary>
        public string BortvalgteSpørsmålDel3 { get; set; }
    }
}
