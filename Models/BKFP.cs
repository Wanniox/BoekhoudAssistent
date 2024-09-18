using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace BoekhoudAssistent.Models
{
	public class BKFP
	{
		//Bedrijfsnummer
		[DisplayName("Bedrijfsnummer")]
		[Required]
		public int BUKRS { get; set; } 

		//Documentnummer boekhoudingsdocument
		[DisplayName("Documentnummer")]
		[Required]
		public int BELNR { get; set; } 

		//Boekjaar
		[DisplayName("Boekjaar")]
		[Required]
		public int GJAHR { get; set; } 

		//Documentsoort
		[DisplayName("Documentsoort")]
		[Required]
		public string BLART { get; set; } 
		
		//Documentdatum
		[DisplayName("Datum")]
		[Required]
		public DateOnly BLDAT { get; set; } 

		//Boekingsdatum in document
		[DisplayName("Boekingsdatum")]
		[Required]
		public DateOnly BUDAT { get; set; } 

		//Boekmaand
		[DisplayName("Boekmaand")]
		[Required]
		[StringLength(2)]
		public string MONAT { get; set; } 

		//Dag waarop boekhoudingsdocument is ingevoerd
		[DisplayName("Dag van invoer")]
		[Required]
		public DateOnly CPUDT { get; set; } 

		//Tijd waarop gegevens zijn ingevoerd
		[DisplayName("Tijd van invoer")]
		[Required]
		public TimeOnly CPUTM { get; set; } 
	}
}
