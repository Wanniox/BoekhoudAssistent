using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BoekhoudAssistent.Models
{
	public class BSEG
	{
		//Bedrijfsnummer
		[DisplayName("Bedrijfsnummer")]
		[Required]
		public int BUKRS { get; set; }

		//Documentnummer boekhoudingsdocument
		[DisplayName("Boekhoudingsnummer")]
		[Required]
		public int BELNR { get; set; }

		//Boekjaar
		[DisplayName("Boekjaar")]
		[Required]
		public int GJAHR { get; set; }

		//Nummer van boekingsregel
		[DisplayName("Regelnummer")]
		[Required]
		public int BUZEI { get; set; }

		//BoekingsID
		[DisplayName("Identificatie")]
		[Required]
		[StringLength(1)]
		public string BUZID { get; set; }

		//Datum van vereffening
		[DisplayName("Vereffeningsdatum")]
		[Required]
		public DateOnly AUGDT { get; set; }

		//Datum van invoer
		[DisplayName("Invoerdatum")]
		[Required]
		public DateOnly AUGCP { get; set; }

		//Documentnummer vereffeningsdocument
		[DisplayName("Vereffeningsnummer")]
		[Required]
		[StringLength(10)]
		public string AUGBL { get; set; }

		//Boekingssleutel
		[DisplayName("Boekingssleutel")]
		[Required]
		[StringLength(2)]
		public string BSCHL { get; set; } 
	}
}
