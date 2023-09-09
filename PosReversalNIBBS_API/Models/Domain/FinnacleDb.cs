namespace PosReversalNIBBS_API.Models.Domain
{
    public class FinnacleDb
    {
        public int Id { get; set; }
        public string  FORACID { get; set; }
        public DateTime? TRAN_DATE { get; set; }
        public string PART_TRAN_TYPE { get; set; }
        public string TRAN_AMT { get; set; }
    }
}
