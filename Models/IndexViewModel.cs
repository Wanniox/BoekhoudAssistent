namespace BoekhoudAssistent.Models
{
    public class IndexViewModel
    {
        public string? Table { get; set; }
        public string? GJAHR { get; set; }
        public string? BUKRS { get; set; }
        public List<string>? BUKRSList { get; set; }
        public string? BELNR { get; set; }
        public string? BELNR2 { get; set; }
        public bool Details { get; set; }
        public List<BKFP> BKFP { get; set; } 
        public List<BSEG> BSEG { get; set; }
    }
}
