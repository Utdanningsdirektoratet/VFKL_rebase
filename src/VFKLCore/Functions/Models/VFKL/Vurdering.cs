using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace VFKLCore.Functions.Models.VFKL
{
    /// <summary>
    /// Represents data model for vuderings form data xml
    /// </summary>
    [XmlRoot(ElementName = "Vurdering")]
    public class Vurdering
    {
        /// <summary>
        /// Type of assessment
        /// </summary>
        [XmlElement("VurderingsType")]
        public string VurderingsType { get; set; }

        /// <summary>
        /// Feide id of bruker
        /// </summary>
        [XmlElement("brukerID")]
        public string BrukerID { get; set; }

        /// <summary>
        /// Type of teaching aid
        /// </summary>
        [XmlElement("Læremiddel")]
        public string Læremiddel { get; set; }

        /// <summary>
        /// Type of teaching aid
        /// </summary>
        [XmlElement("LæremiddelLeverandør")]
        public string LæremiddelLeverandør { get; set; }

        /// <summary>
        /// lesson plan
        /// </summary>
        [XmlElement("Læreplan")]
        public string Læreplan { get; set; }

        /// <summary>
        /// Assessment Id
        /// </summary>
        [XmlElement("VurderingsID")]
        public string VurderingsID { get; set; }

        /// <summary>
        /// Assessment end date
        /// </summary>
        [XmlElement("VurderingsFrist")]
        public string VurderingsFrist { get; set; }

        /// <summary>
        /// GruppeVurdering ID
        /// </summary>
        [XmlElement("GruppeVurderingsID")]
        public string GruppeVurderingsID { get; set; }

        /// <summary>
        /// Name of the user
        /// </summary>
        [XmlElement("Navn")]
        public string Navn { get; set; }

        /// <summary>
        /// All subjects
        /// </summary>
        [XmlElement("AlleFag")]
        public AlleFag AlleFag { get; set; }

        /// <summary>
        /// English
        /// </summary>
        [XmlElement("Engelsk")]
        public Engelsk Engelsk { get; set; }

        /// <summary>
        /// Maths
        /// </summary>
        [XmlElement("Matte")]
        public Matte Matte { get; set; }

        /// <summary>
        /// Norsk
        /// </summary>
        [XmlElement("Norsk")]
        public Norsk Norsk { get; set; }

        /// <summary>
        /// OppsummeringsKommentar
        /// </summary>
        [XmlElement("OppsummeringsKommentar")]
        public string OppsummeringsKommentar { get; set; }
    }

    /// <summary>
    /// All Subjects
    /// </summary>
    public class AlleFag
    {
        /// <summary>
        /// Part 1
        /// </summary>
        [XmlElement("del1")]
        public Del1_AF Del1 { get; set; }

        /// <summary>
        /// PArt 2
        /// </summary>
        [XmlElement("del2")]
        public Del2_AF Del2 { get; set; }

        /// <summary>
        /// Part3
        /// </summary>
        [XmlElement("del3")]
        public Del3_AF Del3 { get; set; }

    }

    /// <summary>
    /// Part 1
    /// </summary>
    public class Del1_AF
    {
        /// <summary>
        /// Not wish to answer
        /// </summary>
        [XmlElement("onskerIkkeSvare")]
        public string OnskerIkkeSvare { get; set; }

        /// <summary>
        /// Q1
        /// </summary>
        [XmlElement("paastand1")]
        public Paastand Paastand1 { get; set; }

        /// <summary>
        /// Q2
        /// </summary>
        [XmlElement("paastand2")]
        public Paastand Paastand2 { get; set; }

        /// <summary>
        /// Q3
        /// </summary>
        [XmlElement("paastand3")]
        public Paastand Paastand3 { get; set; }

        /// <summary>
        /// Q4
        /// </summary>
        [XmlElement("paastand4")]
        public Paastand Paastand4 { get; set; }

    }

    /// <summary>
    /// Answers
    /// </summary>
    public class Paastand
    {
        /// <summary>
        /// Answer
        /// </summary>
        [XmlElement("svar")]
        public string Svar { get; set; }

        /// <summary>
        /// Comment
        /// </summary>
        [XmlElement("kommentar")]
        public string Kommentar { get; set; }

        /// <summary>
        /// Comment
        /// </summary>
        [XmlElement("valgtBortForGruppe")]
        public bool ValgtBortForGruppe { get; set; }
    }

    /// <summary>
    /// Part 2
    /// </summary>
    public class Del2_AF
    {
        /// <summary>
        /// Not wish to answer
        /// </summary>
        [XmlElement("onskerIkkeSvare")]
        public string OnskerIkkeSvare { get; set; }

        /// <summary>
        /// Q1
        /// </summary>
        [XmlElement("paastand1")]
        public Paastand Paastand1 { get; set; }

        /// <summary>
        /// Q2
        /// </summary>
        [XmlElement("paastand2")]
        public Paastand Paastand2 { get; set; }

        /// <summary>
        /// Q3
        /// </summary>
        [XmlElement("paastand3")]
        public Paastand Paastand3 { get; set; }

        /// <summary>
        /// Q4
        /// </summary>
        [XmlElement("paastand4")]
        public Paastand Paastand4 { get; set; }

        /// <summary>
        /// Q5
        /// </summary>
        [XmlElement("paastand5")]
        public Paastand Paastand5 { get; set; }

        /// <summary>
        /// Q6
        /// </summary>
        [XmlElement("paastand6")]
        public Paastand Paastand6 { get; set; }

    }

    /// <summary>
    /// Part 3
    /// </summary>
    public class Del3_AF
    {
        /// <summary>
        /// Not wish to answer
        /// </summary>
        [XmlElement("onskerIkkeSvare")]
        public string OnskerIkkeSvare { get; set; }

        /// <summary>
        /// Q1
        /// </summary>
        [XmlElement("paastand1")]
        public Paastand Paastand1 { get; set; }

        /// <summary>
        /// Q2
        /// </summary>
        [XmlElement("paastand2")]
        public Paastand Paastand2 { get; set; }

        /// <summary>
        /// Q3
        /// </summary>
        [XmlElement("paastand3")]
        public Paastand Paastand3 { get; set; }

        /// <summary>
        /// Q4
        /// </summary>
        [XmlElement("paastand4")]
        public Paastand Paastand4 { get; set; }

        /// <summary>
        /// Q5
        /// </summary>
        [XmlElement("paastand5")]
        public Paastand Paastand5 { get; set; }

        /// <summary>
        /// Q6
        /// </summary>
        [XmlElement("paastand6")]
        public Paastand Paastand6 { get; set; }

        /// <summary>
        /// Q7
        /// </summary>
        [XmlElement("paastand7")]
        public Paastand Paastand7 { get; set; }

        /// <summary>
        /// Q8
        /// </summary>
        [XmlElement("paastand8")]
        public Paastand Paastand8 { get; set; }
    }

    /// <summary>
    /// Engelsk fag
    /// </summary>
    public class Engelsk
    {
        /// <summary>
        /// Part 1
        /// </summary>
        [XmlElement("del1")]
        public Del1_En Del1 { get; set; }

        /// <summary>
        /// Part 2
        /// </summary>
        [XmlElement("del2")]
        public Del2_En Del2 { get; set; }

        /// <summary>
        /// Part 3
        /// </summary>
        [XmlElement("del3")]
        public Del3_En Del3 { get; set; }
    }

    /// <summary>
    /// Part1
    /// </summary>
    public class Del1_En
    {
        /// <summary>
        /// Q1
        /// </summary>
        [XmlElement("paastand1")]
        public Paastand Paastand1 { get; set; }

        /// <summary>
        /// Q2
        /// </summary>
        [XmlElement("paastand2")]
        public Paastand Paastand2 { get; set; }

        /// <summary>
        /// Q3
        /// </summary>
        [XmlElement("paastand3")]
        public Paastand Paastand3 { get; set; }

        /// <summary>
        /// Q4
        /// </summary>
        [XmlElement("paastand4")]
        public Paastand Paastand4 { get; set; }

        /// <summary>
        /// Q5
        /// </summary>
        [XmlElement("paastand5")]
        public Paastand Paastand5 { get; set; }

        /// <summary>
        /// Q6
        /// </summary>
        [XmlElement("paastand6")]
        public Paastand Paastand6 { get; set; }

        /// <summary>
        /// Q7
        /// </summary>
        [XmlElement("paastand7")]
        public Paastand Paastand7 { get; set; }

        /// <summary>
        /// Q8
        /// </summary>
        [XmlElement("paastand8")]
        public Paastand Paastand8 { get; set; }

        /// <summary>
        /// Q9
        /// </summary>
        [XmlElement("paastand9")]
        public Paastand Paastand9 { get; set; }

        /// <summary>
        /// Q10
        /// </summary>
        [XmlElement("paastand10")]
        public Paastand Paastand10 { get; set; }

        /// <summary>
        /// Q11
        /// </summary>
        [XmlElement("paastand11")]
        public Paastand Paastand11 { get; set; }

        /// <summary>
        /// Q12
        /// </summary>
        [XmlElement("paastand12")]
        public Paastand Paastand12 { get; set; }

    }

    /// <summary>
    /// Part2 English
    /// </summary>
    public class Del2_En
    {
        /// <summary>
        /// Q1
        /// </summary>
        [XmlElement("paastand1")]
        public Paastand Paastand1 { get; set; }

        /// <summary>
        /// Q2
        /// </summary>
        [XmlElement("paastand2")]
        public Paastand Paastand2 { get; set; }

        /// <summary>
        /// Q3
        /// </summary>
        [XmlElement("paastand3")]
        public Paastand Paastand3 { get; set; }

        /// <summary>
        /// Q4
        /// </summary>
        [XmlElement("paastand4")]
        public Paastand Paastand4 { get; set; }

        /// <summary>
        /// Q5
        /// </summary>
        [XmlElement("paastand5")]
        public Paastand Paastand5 { get; set; }

        /// <summary>
        /// Q6
        /// </summary>
        [XmlElement("paastand6")]
        public Paastand Paastand6 { get; set; }

        /// <summary>
        /// Q7
        /// </summary>
        [XmlElement("paastand7")]
        public Paastand Paastand7 { get; set; }

        /// <summary>
        /// Q8
        /// </summary>
        [XmlElement("paastand8")]
        public Paastand Paastand8 { get; set; }

        /// <summary>
        /// Q9
        /// </summary>
        [XmlElement("paastand9")]
        public Paastand Paastand9 { get; set; }

        /// <summary>
        /// Q10
        /// </summary>
        [XmlElement("paastand10")]
        public Paastand Paastand10 { get; set; }

        /// <summary>
        /// Q11
        /// </summary>
        [XmlElement("paastand11")]
        public Paastand Paastand11 { get; set; }

        /// <summary>
        /// Q12
        /// </summary>
        [XmlElement("paastand12")]
        public Paastand Paastand12 { get; set; }

        /// <summary>
        /// Q13
        /// </summary>
        [XmlElement("paastand13")]
        public Paastand Paastand13 { get; set; }

        /// <summary>
        /// Q14
        /// </summary>
        [XmlElement("paastand14")]
        public Paastand Paastand14 { get; set; }
    }

    /// <summary>
    /// Del 3 English
    /// </summary>
    public class Del3_En
    {
        /// <summary>
        /// Q1
        /// </summary>
        [XmlElement("paastand1")]
        public Paastand Paastand1 { get; set; }

        /// <summary>
        /// Q2
        /// </summary>
        [XmlElement("paastand2")]
        public Paastand Paastand2 { get; set; }

        /// <summary>
        /// Q3
        /// </summary>
        [XmlElement("paastand3")]
        public Paastand Paastand3 { get; set; }

        /// <summary>
        /// Q4
        /// </summary>
        [XmlElement("paastand4")]
        public Paastand Paastand4 { get; set; }
    }

    /// <summary>
    /// Maths fag
    /// </summary>
    public class Matte
    {
        /// <summary>
        /// Part 1
        /// </summary>
        [XmlElement("del1")]
        public Del1_Ma Del1 { get; set; }

        /// <summary>
        /// Part 2
        /// </summary>
        [XmlElement("del2")]
        public Del2_Ma Del2 { get; set; }

        /// <summary>
        /// Part 3
        /// </summary>
        [XmlElement("del3")]
        public Del3_Ma Del3 { get; set; }
    }

    /// <summary>
    /// Del 1 Maths
    /// </summary>
    public class Del1_Ma
    {
        /// <summary>
        /// Q1
        /// </summary>
        [XmlElement("paastand1")]
        public Paastand Paastand1 { get; set; }

        /// <summary>
        /// Q2
        /// </summary>
        [XmlElement("paastand2")]
        public Paastand Paastand2 { get; set; }

        /// <summary>
        /// Q3
        /// </summary>
        [XmlElement("paastand3")]
        public Paastand Paastand3 { get; set; }

        /// <summary>
        /// Q4
        /// </summary>
        [XmlElement("paastand4")]
        public Paastand Paastand4 { get; set; }

        /// <summary>
        /// Q5
        /// </summary>
        [XmlElement("paastand5")]
        public Paastand Paastand5 { get; set; }

        /// <summary>
        /// Q6
        /// </summary>
        [XmlElement("paastand6")]
        public Paastand Paastand6 { get; set; }

        /// <summary>
        /// Q7
        /// </summary>
        [XmlElement("paastand7")]
        public Paastand Paastand7 { get; set; }
    }

    /// <summary>
    /// Part 2 Maths
    /// </summary>
    public class Del2_Ma
    {
        /// <summary>
        /// Q1
        /// </summary>
        [XmlElement("paastand1")]
        public Paastand Paastand1 { get; set; }

        /// <summary>
        /// Q2
        /// </summary>
        [XmlElement("paastand2")]
        public Paastand Paastand2 { get; set; }

        /// <summary>
        /// Q3
        /// </summary>
        [XmlElement("paastand3")]
        public Paastand Paastand3 { get; set; }

        /// <summary>
        /// Q4
        /// </summary>
        [XmlElement("paastand4")]
        public Paastand Paastand4 { get; set; }

        /// <summary>
        /// Q5
        /// </summary>
        [XmlElement("paastand5")]
        public Paastand Paastand5 { get; set; }

        /// <summary>
        /// Q6
        /// </summary>
        [XmlElement("paastand6")]
        public Paastand Paastand6 { get; set; }
    }

    /// <summary>
    /// PArt 3 Maths
    /// </summary>
    public class Del3_Ma
    {
        /// <summary>
        /// Q1
        /// </summary>
        [XmlElement("paastand1")]
        public Paastand Paastand1 { get; set; }

        /// <summary>
        /// Q2
        /// </summary>
        [XmlElement("paastand2")]
        public Paastand Paastand2 { get; set; }

        /// <summary>
        /// Q3
        /// </summary>
        [XmlElement("paastand3")]
        public Paastand Paastand3 { get; set; }

        /// <summary>
        /// Q4
        /// </summary>
        [XmlElement("paastand4")]
        public Paastand Paastand4 { get; set; }

        /// <summary>
        /// Q5
        /// </summary>
        [XmlElement("paastand5")]
        public Paastand Paastand5 { get; set; }

        /// <summary>
        /// Q6
        /// </summary>
        [XmlElement("paastand6")]
        public Paastand Paastand6 { get; set; }

        /// <summary>
        /// Q7
        /// </summary>
        [XmlElement("paastand7")]
        public Paastand Paastand7 { get; set; }

        /// <summary>
        /// Q8
        /// </summary>
        [XmlElement("paastand8")]
        public Paastand Paastand8 { get; set; }

        /// <summary>
        /// Q9
        /// </summary>
        [XmlElement("paastand9")]
        public Paastand Paastand9 { get; set; }

        /// <summary>
        /// Q10
        /// </summary>
        [XmlElement("paastand10")]
        public Paastand Paastand10 { get; set; }

        /// <summary>
        /// Q11
        /// </summary>
        [XmlElement("paastand11")]
        public Paastand Paastand11 { get; set; }

    }

    /// <summary>
    /// Norsk fag
    /// </summary>
    public class Norsk
    {
        /// <summary>
        /// Part1
        /// </summary>
        [XmlElement("del1")]
        public Del1_No Del1 { get; set; }

        /// <summary>
        /// Part 2
        /// </summary>
        [XmlElement("del2")]
        public Del2_No Del2 { get; set; }

        /// <summary>
        /// Del3
        /// </summary>
        [XmlElement("del3")]
        public Del3_No Del3 { get; set; }
    }

    /// <summary>
    /// Part 1 Norsk
    /// </summary>
    public class Del1_No
    {
        /// <summary>
        /// Q1
        /// </summary>
        [XmlElement("paastand1")]
        public Paastand Paastand1 { get; set; }

        /// <summary>
        /// Q2
        /// </summary>
        [XmlElement("paastand2")]
        public Paastand Paastand2 { get; set; }

        /// <summary>
        /// Q3
        /// </summary>
        [XmlElement("paastand3")]
        public Paastand Paastand3 { get; set; }

        /// <summary>
        /// Q4
        /// </summary>
        [XmlElement("paastand4")]
        public Paastand Paastand4 { get; set; }

        /// <summary>
        /// Q5
        /// </summary>
        [XmlElement("paastand5")]
        public Paastand Paastand5 { get; set; }

        /// <summary>
        /// Q6
        /// </summary>
        [XmlElement("paastand6")]
        public Paastand Paastand6 { get; set; }

        /// <summary>
        /// Q7
        /// </summary>
        [XmlElement("paastand7")]
        public Paastand Paastand7 { get; set; }

        /// <summary>
        /// Q8
        /// </summary>
        [XmlElement("paastand8")]
        public Paastand Paastand8 { get; set; }

        /// <summary>
        /// Q9
        /// </summary>
        [XmlElement("paastand9")]
        public Paastand Paastand9 { get; set; }

        /// <summary>
        /// Q10
        /// </summary>
        [XmlElement("paastand10")]
        public Paastand Paastand10 { get; set; }

        /// <summary>
        /// Q11
        /// </summary>
        [XmlElement("paastand11")]
        public Paastand Paastand11 { get; set; }

        /// <summary>
        /// Q12
        /// </summary>
        [XmlElement("paastand12")]
        public Paastand Paastand12 { get; set; }

        /// <summary>
        /// Q13
        /// </summary>
        [XmlElement("paastand13")]
        public Paastand Paastand13 { get; set; }

        /// <summary>
        /// Q14
        /// </summary>
        [XmlElement("paastand14")]
        public Paastand Paastand14 { get; set; }
    }

    /// <summary>
    /// Part 2 Norsk
    /// </summary>
    public class Del2_No
    {
        /// <summary>
        /// Q1
        /// </summary>
        [XmlElement("paastand1")]
        public Paastand Paastand1 { get; set; }

        /// <summary>
        /// Q2
        /// </summary>
        [XmlElement("paastand2")]
        public Paastand Paastand2 { get; set; }

        /// <summary>
        /// Q3
        /// </summary>
        [XmlElement("paastand3")]
        public Paastand Paastand3 { get; set; }

        /// <summary>
        /// Q4
        /// </summary>
        [XmlElement("paastand4")]
        public Paastand Paastand4 { get; set; }
    }

    /// <summary>
    /// Part 3 Norsk
    /// </summary>
    public class Del3_No
    {
        /// <summary>
        /// Q1
        /// </summary>
        [XmlElement("paastand1")]
        public Paastand Paastand1 { get; set; }

        /// <summary>
        /// Q2
        /// </summary>
        [XmlElement("paastand2")]
        public Paastand Paastand2 { get; set; }

        /// <summary>
        /// Q3
        /// </summary>
        [XmlElement("paastand3")]
        public Paastand Paastand3 { get; set; }

        /// <summary>
        /// Q4
        /// </summary>
        [XmlElement("paastand4")]
        public Paastand Paastand4 { get; set; }

        /// <summary>
        /// Q5
        /// </summary>
        [XmlElement("paastand5")]
        public Paastand Paastand5 { get; set; }
    }
}
