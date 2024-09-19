namespace BoekhoudAssistent.Models
{
    public class IndexViewModel
    {
        public string? Table { get; set; }
        public List<string>? GJAHRList { get; set; }
        public List<string>? BUKRSList { get; set; }
        public List<string>? BELNRList { get; set; }
        public bool Details { get; set; }
        public List<BKFP> BKFP { get; set; } 
        public List<BSEG> BSEG { get; set; }
    }
}
